using SipStack.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class TimeZoneAdjustment
    {
        public TimeZoneAdjustment(long time, long offset)
        {
            Time = time;
            Offset = offset;
        }

        public long Time { get; private set; }
        public long Offset { get; private set; }

        public static ParseResult<List<TimeZoneAdjustment>> Parse(string data)
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
                    return ParseResult<List<TimeZoneAdjustment>>.CreateError($"could not parse {timeStampString} as integer");

                if (!long.TryParse(offsetString, out offset))
                    return ParseResult<List<TimeZoneAdjustment>>.CreateError($"could not parse {offsetString} as integer");

                if (!TimeUnit.ApplyUnit(offset, unit, out offsetWithUnit))
                    return ParseResult<List<TimeZoneAdjustment>>.CreateError($"use of invalid time unit in {data}");

                timeZoneAdjustments.Add(new TimeZoneAdjustment(timeStamp, offsetWithUnit));
            }

            return ParseResult<List<TimeZoneAdjustment>>.CreateSuccess(timeZoneAdjustments);
        }
    }
}
