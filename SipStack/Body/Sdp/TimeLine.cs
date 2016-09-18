using System;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class TimeLine : ILine
    {
        private readonly DateTime _start;
        private readonly DateTime _end;

        public TimeLine(DateTime start, DateTime end)
        {
            if (DateTime.Compare(start, end) >= 0)
                throw new ArgumentException("end must be after start");

            _start = start;
            _end = end;
        }

        public DateTime Start => _start;
        public DateTime End => _end;

        public static ParseResult<ILine> CreateFrom(string data)
        {
            var pattern = @"^([0-9]*) ([0-9]*)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return new ParseResult<ILine>($"the data '{data}' for the time schedule is inalid");

            var match = matches[0];
            var startString = match.Groups[1].Value;
            var endString = match.Groups[2].Value;
            long startInteger;
            long endInteger;

            if (!long.TryParse(startString, out startInteger))
                return new ParseResult<ILine>($"the value '{startString}' for start is invalid");

            if (!long.TryParse(endString, out endInteger))
                return new ParseResult<ILine>($"the value '{endInteger}' for start is invalid");

            if (startInteger <= 0)
                return new ParseResult<ILine>("the value for start must not be negative");

            if (endInteger < 0)
                return new ParseResult<ILine>("the value for start must be positive");

            if (endInteger > 0 && startInteger > endInteger)
                return new ParseResult<ILine>("the value for start must be less or equal than end");

            var start = new DateTime(startInteger);
            var end = DateTime.MaxValue;

            if (endInteger > 0)
                end = new DateTime(endInteger);

            return new ParseResult<ILine>(new TimeLine(start, end));
        }
    }
}
