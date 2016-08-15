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
            int lastHeaderLine;

            var headerParseResult = ParseHeader(lines, out lastHeaderLine);

            if (headerParseResult.IsError)
                return headerParseResult.ToParseResult<Message>();

            var header = headerParseResult.Result;

            if (lastHeaderLine < 0)
                return new ParseResult<Message>(ParseError.ContentLengthLineMissing, "the content length field is missing");

            if (header.ContentLength < 0)
                return new ParseResult<Message>(ParseError.NegativeValueForContentLength, "the content length must not have a negative value");

            if (lastHeaderLine + 1 >= lines.Count)
                return new ParseResult<Message>(ParseError.CrlfAtEndMissing, "the CRLF after the content length field is missing");

            if (header.ContentLength == 0)
                return new ParseResult<Message>(new Message(header, new NoBody()));
            
            var bodyResult = _bodyParser.Parse(lines, lastHeaderLine + 2, lines.Count() - 1);
            if (bodyResult.IsError)
                return bodyResult.ToParseResult<Message>();

            return new ParseResult<Message>(new Message(header, bodyResult.Result));
        }

        private ParseResult<Header> ParseHeader(IList<string> lines, out int lastHeaderLine)
        {
            lastHeaderLine = -1;
            var headerFields = new List<HeaderField>();

            var requestLineResult = _requestLineParser.Parse(lines[0]);
            if (requestLineResult.IsError)
                return requestLineResult.ToParseResult<Header>();

            for (var i = 1; i < lines.Count(); ++i)
            {
                var currentLine = lines[i];

                if (string.IsNullOrEmpty(currentLine))
                {
                    lastHeaderLine = i - 1;
                    break;
                }

                var headerFieldResult = _headerFieldParser.Parse(lines, i);

                if (headerFieldResult.IsError)
                    return headerFieldResult.ToParseResult<Header>();

                var headerField = headerFieldResult.Result;
                headerFields.Add(headerField);
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
