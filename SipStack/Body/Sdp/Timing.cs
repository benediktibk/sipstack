using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SipStack.Body.Sdp
{
    public class Timing
    {
        public Timing(DateTime start, DateTime end)
        {
            if (DateTime.Compare(start, end) >= 0)
                throw new ArgumentException("end must be after start");

            Start = start;
            End = end;
        }

        public DateTime Start { get; }
        public DateTime End { get; }

        public static ParseResult<Timing> Parse(string data)
        {
            var pattern = @"^([0-9]*) ([0-9]*)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return new ParseResult<Timing>($"the data '{data}' for the time schedule is inalid");

            var match = matches[0];
            var startString = match.Groups[1].Value;
            var endString = match.Groups[2].Value;
            long startInteger;
            long endInteger;

            if (!long.TryParse(startString, out startInteger))
                return new ParseResult<Timing>($"the value '{startString}' for start is invalid");

            if (!long.TryParse(endString, out endInteger))
                return new ParseResult<Timing>($"the value '{endInteger}' for start is invalid");

            if (startInteger < 0)
                return new ParseResult<Timing>("the value for start must not be negative");

            if (endInteger < 0)
                return new ParseResult<Timing>("the value for start must be positive");

            if (endInteger > 0 && startInteger > endInteger)
                return new ParseResult<Timing>("the value for start must be less or equal than end");

            var start = DateTimeHelper.NtpTimeStampToDateTime(startInteger);
            var end = DateTime.MaxValue;

            if (endInteger > 0)
                end = DateTimeHelper.NtpTimeStampToDateTime(endInteger);

            return new ParseResult<Timing>(new Timing(start, end));
        }
    }
}
