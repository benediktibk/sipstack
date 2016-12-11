using SipStack.Utils;
using System;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class RepeatLine : ILine
    {
        #region constructors

        public RepeatLine(TimeSpan repeatInterval, TimeSpan activeDuration, TimeSpan offsetStart, TimeSpan offsetEnd)
        {
            Repeat = new Repeat(repeatInterval, activeDuration, offsetStart, offsetEnd);
        }

        #endregion

        #region properties

        public Repeat Repeat { get; }

        #endregion

        #region static functions

        public static ParseResult<ILine> Parse(string data)
        {
            var pattern = @"^([0-9dhms]*) ([0-9dhms]*) ([0-9dhms]*) ([0-9dhms]*)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return new ParseResult<ILine>($"the data '{data}' for the repeat line is malformed");

            var match = matches[0];
            var repeatInterval = CreateTimeSpanFrom(match.Groups[1].Value);
            var activeDuration = CreateTimeSpanFrom(match.Groups[2].Value);
            var offsetStart = CreateTimeSpanFrom(match.Groups[3].Value);
            var offsetEnd = CreateTimeSpanFrom(match.Groups[4].Value);

            if (repeatInterval.IsError)
                return repeatInterval.ToParseResult<ILine>();

            if (activeDuration.IsError)
                return activeDuration.ToParseResult<ILine>();

            if (offsetStart.IsError)
                return offsetStart.ToParseResult<ILine>();

            if (offsetEnd.IsError)
                return offsetEnd.ToParseResult<ILine>();

            if (TimeSpan.Compare(offsetStart.Result, offsetEnd.Result) > 0)
                return new ParseResult<ILine>("the value for offset start must be less or equal than the value for offset end");

            return new ParseResult<ILine>(new RepeatLine(repeatInterval.Result, activeDuration.Result, offsetStart.Result, offsetEnd.Result));
        }

        public static ParseResult<TimeSpan> CreateTimeSpanFrom(string data)
        {
            var pattern = @"^([0-9]*)([dhms]?)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return new ParseResult<TimeSpan>($"invalid format for time span: {data}");

            var match = matches[0];
            var valueString = match.Groups[1].Value;
            var unit = match.Groups[2].Value;
            long value;
            long valueWithUnit;

            if (!long.TryParse(valueString, out value))
                return new ParseResult<TimeSpan>($"the value {valueString} for the time span is malformed");

            if (!TimeUnit.ApplyUnit(value, unit, out valueWithUnit))
                return new ParseResult<TimeSpan>($"the unit {unit} is invalid");

            return new ParseResult<TimeSpan>(TimeSpan.FromSeconds(valueWithUnit));
        }

        #endregion
    }
}
