using System.Collections.Generic;
using System.Linq;

namespace SipStack
{
    public class MessageParser
    {
        private RequestLineParser _requestLineParser;
        private HeaderFieldParser _headerFieldParser;
        private BodyParser _bodyParser;

        public MessageParser(RequestLineParser requestLineParser, HeaderFieldParser headerFieldParser, BodyParser bodyParser) 
        {
            _requestLineParser = requestLineParser;
            _headerFieldParser = headerFieldParser;
            _bodyParser = bodyParser;
        }

        public ParseResult<Message> Parse(string message)
        {
            var lines = SplitLines(message);
            int contentLengthLine;

            var headerParseResult = ParseHeader(lines, out contentLengthLine);

            if (headerParseResult.IsError)
                return headerParseResult.ToParseResult<Message>();

            var header = headerParseResult.Result;

            if (contentLengthLine < 0)
                return new ParseResult<Message>(ParseError.ContentLengthLineMissing, "the content length field is missing");

            if (header.ContentLength < 0)
                return new ParseResult<Message>(ParseError.NegativeValueForContentLength, "the content length must not have a negative value");

            if (contentLengthLine + 1 >= lines.Count)
                return new ParseResult<Message>(ParseError.CrlfAtEndMissing, "the CRLF after the content length field is missing");

            if (lines[contentLengthLine + 1] != "")
                return new ParseResult<Message>(ParseError.HeadersAfterContentLength, "there are additional headers after the content length field");

            if (header.ContentLength == 0)
                return new ParseResult<Message>(new Message(header, new NoBody()));
            
            var bodyResult = _bodyParser.Parse(lines, contentLengthLine + 2, lines.Count() - 1);
            if (bodyResult.IsError)
                return bodyResult.ToParseResult<Message>();

            return new ParseResult<Message>(new Message(header, bodyResult.Result));
        }

        private ParseResult<Header> ParseHeader(IList<string> lines, out int contentLengthLine)
        {
            contentLengthLine = -1;
            var headerFields = new List<HeaderField>();

            var requestLineResult = _requestLineParser.Parse(lines[0]);
            if (requestLineResult.IsError)
                return requestLineResult.ToParseResult<Header>();

            for (var i = 1; i < lines.Count() && contentLengthLine < 0; ++i)
            {
                var currentLine = lines[i];
                var headerFieldResult = _headerFieldParser.Parse(lines, i);

                if (headerFieldResult.IsError)
                    return headerFieldResult.ToParseResult<Header>();

                var headerField = headerFieldResult.Result;
                headerFields.Add(headerField);

                if (headerField.Name.isOfType(HeaderFieldType.ContentLength))
                    contentLengthLine = i;
            }

            return Header.CreateFrom(requestLineResult.Result, headerFields);
        }

        private static IList<string> SplitLines(string message)
        {
            var lines = message.Split('\n'); ;

            for(var i = 0; i < lines.Count(); ++i)
            {
                var line = lines[i];

                if (string.IsNullOrEmpty(line))
                    continue;

                if (line[line.Length - 1] != '\r')
                    continue;

                lines[i] = line.Substring(0, line.Length - 1);
            }

            return lines;
        }
    }
}
