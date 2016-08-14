namespace SipStack
{
    public class HeaderFieldParser
    {
        public ParseResult<HeaderField> Parse(string line)
        {
            var indexOfDelimiter = line.IndexOf(':');

            if (indexOfDelimiter <= 0)
                return new ParseResult<HeaderField>(ParseError.InvalidHeaderField, $"invalid header field: {line}");

            var fieldName = line.Substring(0, indexOfDelimiter - 1);
            var fieldValue = line.Substring(indexOfDelimiter + 1);

            return new ParseResult<HeaderField>(new HeaderField { Name = fieldName, Value = fieldValue });
        }
    }
}
