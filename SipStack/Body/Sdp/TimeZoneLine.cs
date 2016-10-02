using SipStack.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class TimeZoneLine : ILine
    {
        private readonly List<TimeZoneAdjustment> _timeZoneAdjustments;

        public TimeZoneLine(IReadOnlyList<TimeZoneAdjustment> timeZoneAdjustments)
        {
            _timeZoneAdjustments = timeZoneAdjustments.ToList();
        }

        public IReadOnlyList<TimeZoneAdjustment> TimeZoneAdjustments => _timeZoneAdjustments;

        public static ParseResult<ILine> Parse(string data)
        {
            var pattern = @"([0-9]+) ([\-]?[0-9]+)([dhms]?)";
            var matches = Regex.Matches(data, pattern);
            var timeZoneAdjustments = new List<TimeZoneAdjustment>(matches.Count);

            foreach (Match match in matches)
            {
                var timeStampString = match.Groups[1].Value;
                var offsetString = match.Groups[2].Value;
                var unit = match.Groups[3].Value;

                long offsetWithUnit;
                long offset;
                long timeStamp;

                if (!long.TryParse(timeStampString, out timeStamp))
                    return new ParseResult<ILine>($"could not parse {timeStampString} as integer");

                if (!long.TryParse(offsetString, out offset))
                    return new ParseResult<ILine>($"could not parse {offsetString} as integer");

                if (!TimeUnit.ApplyUnit(offset, unit, out offsetWithUnit))
                    return new ParseResult<ILine>($"use of invalid time unit in {data}");

                timeZoneAdjustments.Add(new TimeZoneAdjustment(timeStamp, offsetWithUnit));
            }

            return new ParseResult<ILine>(new TimeZoneLine(timeZoneAdjustments));
        }
    }
}
