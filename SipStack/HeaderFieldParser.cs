using System;
using System.Collections.Generic;

namespace SipStack
{
    public class HeaderFieldParser
    {
        public ParseResult<HeaderField> Parse(IList<string> lines, int start)
        {
            var startLine = lines[start];

            if (string.IsNullOrEmpty(startLine))
                return new ParseResult<HeaderField>(ParseError.InvalidHeaderField, $"empty header line");

            var indexOfDelimiter = startLine.IndexOf(':');
            var indexOfWhitespace = startLine.IndexOf(' ');

            if (indexOfDelimiter < 0)
                return new ParseResult<HeaderField>(ParseError.InvalidHeaderField, $"missing colon in header field: {startLine}");

            var nameStart = 0;
            var nameEnd = 0;
            var valueStart = 0;
            var valueEnd = 0;

            if (indexOfWhitespace < 0)
            {
                nameStart = 0;
                nameEnd = indexOfDelimiter - 1;
                valueStart = indexOfDelimiter + 1;
                valueEnd = startLine.Length - 1;
            }
            else if (indexOfWhitespace > indexOfDelimiter)
            {
                nameStart = 0;
                nameEnd = indexOfDelimiter - 1;
                var indexOfNoneWhitespace = IndexOfNoneWhitespace(startLine, indexOfWhitespace);

                if (indexOfNoneWhitespace < 0)
                    indexOfNoneWhitespace = startLine.Length;

                valueStart = indexOfNoneWhitespace;
                valueEnd = startLine.Length - 1;
            }
            else
            {
                nameStart = 0;
                nameEnd = indexOfWhitespace - 1;
                var indexOfNoneWhitespace = IndexOfNoneWhitespace(startLine, indexOfDelimiter + 1);

                if (indexOfNoneWhitespace < 0)
                    indexOfNoneWhitespace = startLine.Length;

                valueStart = indexOfNoneWhitespace;
                valueEnd = startLine.Length - 1;
            }

            var fieldName = startLine.Substring(nameStart, nameEnd - nameStart + 1);
            var fieldValue = startLine.Substring(valueStart, valueEnd - valueStart + 1);

            return new ParseResult<HeaderField>(new HeaderField { Name = fieldName, Value = fieldValue });
        }

        private static int IndexOfNoneWhitespace(string line, int start)
        {
            for (var i = start; i < line.Length; ++i)
            {
                var current = line[i];

                if (current != ' ' && current != '\t')
                    return i;
            }

            return -1;
        }
    }
}
