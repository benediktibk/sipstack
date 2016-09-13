using SipStack.Body;
using SipStack.Header;
using System.Collections.Generic;
using System.Linq;

namespace SipStack
{
    public class MessageParser
    {
        private RequestLineParser _requestLineParser;
        private HeaderFieldParser _headerFieldParser;
        private BodyParserFactory _bodyParserFactory;

        public MessageParser(RequestLineParser requestLineParser, HeaderFieldParser headerFieldParser, BodyParserFactory bodyParserFactory) 
        {
            _requestLineParser = requestLineParser;
            _headerFieldParser = headerFieldParser;
            _bodyParserFactory = bodyParserFactory;
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
                return new ParseResult<Message>("the content length field is missing");

            if (header.ContentLength < 0)
                return new ParseResult<Message>("the content length must not have a negative value");

            if (lastHeaderLine + 1 >= lines.Count)
                return new ParseResult<Message>("the CRLF after the content length field is missing");

            if (header.ContentLength == 0)
                return new ParseResult<Message>(new Message(header, new NoBody()));

            var bodyParser = _bodyParserFactory.Create(header.ContentType);
            var bodyResult = bodyParser.Parse(lines, lastHeaderLine + 2, lines.Count() - 1);
            if (bodyResult.IsError)
                return bodyResult.ToParseResult<Message>();

            return new ParseResult<Message>(new Message(header, bodyResult.Result));
        }

        private ParseResult<Header.Header> ParseHeader(IList<string> lines, out int lastHeaderLine)
        {
            lastHeaderLine = -1;
            var headerFields = new List<HeaderField>();

            var requestLineResult = _requestLineParser.Parse(lines[0]);
            if (requestLineResult.IsError)
                return requestLineResult.ToParseResult<Header.Header>();

            for (var i = 1; i < lines.Count(); ++i)
            {
                var currentLine = lines[i];

                if (string.IsNullOrEmpty(currentLine))
                {
                    lastHeaderLine = i - 1;
                    break;
                }

                int end;
                var headerFieldResult = _headerFieldParser.Parse(lines, i, out end);

                if (headerFieldResult.IsError)
                    return headerFieldResult.ToParseResult<Header.Header>();

                var headerField = headerFieldResult.Result;
                headerFields.Add(headerField);
                i = end;
            }

            return Header.Header.CreateFrom(requestLineResult.Result, headerFields);
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
