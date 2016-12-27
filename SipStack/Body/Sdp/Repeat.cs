using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SipStack.Body.Sdp
{
    public class Repeat
    {
        #region constructors

        public Repeat(TimeSpan repeatInterval, TimeSpan activeDuration, TimeSpan offsetStart, TimeSpan offsetEnd)
        {
            if (TimeSpan.Compare(offsetStart, offsetEnd) > 0)
                throw new ArgumentException("start must be before end");

            RepeatInterval = repeatInterval;
            ActiveDuration = activeDuration;
            OffsetStart = offsetStart;
            OffsetEnd = offsetEnd;
        }

        #endregion

        #region properties

        public TimeSpan RepeatInterval { get; }
        public TimeSpan ActiveDuration { get; }
        public TimeSpan OffsetStart { get; }
        public TimeSpan OffsetEnd { get; }

        #endregion

        #region static functions

        public static ParseResult<Repeat> Parse(string data)
        {
            var pattern = @"^([0-9dhms]*) ([0-9dhms]*) ([0-9dhms]*) ([0-9dhms]*)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return ParseResult<Repeat>.CreateError($"the data '{data}' for the repeat line is malformed");

            var match = matches[0];
            var repeatInterval = CreateTimeSpanFrom(match.Groups[1].Value);
            var activeDuration = CreateTimeSpanFrom(match.Groups[2].Value);
            var offsetStart = CreateTimeSpanFrom(match.Groups[3].Value);
            var offsetEnd = CreateTimeSpanFrom(match.Groups[4].Value);

            if (repeatInterval.IsError)
                return repeatInterval.ToParseResult<Repeat>();

            if (activeDuration.IsError)
                return activeDuration.ToParseResult<Repeat>();

            if (offsetStart.IsError)
                return offsetStart.ToParseResult<Repeat>();

            if (offsetEnd.IsError)
                return offsetEnd.ToParseResult<Repeat>();

            if (TimeSpan.Compare(offsetStart.Result, offsetEnd.Result) > 0)
                return ParseResult<Repeat>.CreateError("the value for offset start must be less or equal than the value for offset end");

            return ParseResult<Repeat>.CreateSuccess(new Repeat(repeatInterval.Result, activeDuration.Result, offsetStart.Result, offsetEnd.Result));
        }

        public static ParseResult<TimeSpan> CreateTimeSpanFrom(string data)
        {
            var pattern = @"^([0-9]*)([dhms]?)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return ParseResult<TimeSpan>.CreateError($"invalid format for time span: {data}");

            var match = matches[0];
            var valueString = match.Groups[1].Value;
            var unit = match.Groups[2].Value;
            long value;
            long valueWithUnit;

            if (!long.TryParse(valueString, out value))
                return ParseResult<TimeSpan>.CreateError($"the value {valueString} for the time span is malformed");

            if (!TimeUnit.ApplyUnit(value, unit, out valueWithUnit))
                return ParseResult<TimeSpan>.CreateError($"the unit {unit} is invalid");

            return ParseResult<TimeSpan>.CreateSuccess(TimeSpan.FromSeconds(valueWithUnit));
        }

        #endregion
    }
}
