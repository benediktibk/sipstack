using System.Collections.Generic;

namespace SipStack
{
    public class HeaderFieldParser
    {
        public ParseResult<HeaderField> Parse(IList<string> lines, int start)
        {
            var startLine = lines[start];
            var indexOfDelimiter = startLine.IndexOf(':');

            if (indexOfDelimiter <= 0)
                return new ParseResult<HeaderField>(ParseError.InvalidHeaderField, $"invalid header field: {startLine}");

            var fieldName = startLine.Substring(0, indexOfDelimiter - 1);
            var fieldValue = startLine.Substring(indexOfDelimiter + 1);

            return new ParseResult<HeaderField>(new HeaderField { Name = fieldName, Value = fieldValue });
        }
    }
}
