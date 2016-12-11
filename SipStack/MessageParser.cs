using SipStack.Body;
using SipStack.Header;
using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack
{
    public class MessageParser
    {
        private readonly HeaderParser _headerParser;
        private readonly BodyParserFactory _bodyParserFactory;

        public MessageParser(HeaderParser headerParser, BodyParserFactory bodyParserFactory) 
        {
            _headerParser = headerParser;
            _bodyParserFactory = bodyParserFactory;
        }

        public ParseResult<Message> Parse(string message)
        {
            var lines = message.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            int lastHeaderLine;

            var headerParseResult = ParseHeader(lines, out lastHeaderLine);

            if (headerParseResult.IsError)
                return headerParseResult.ToParseResult<Message>();

            var header = headerParseResult.Result;

            if (lastHeaderLine < 0)
                return new ParseResult<Message>("the content length field is missing");

            if (header.ContentLength < 0)
                return new ParseResult<Message>("the content length must not have a negative value");

            if (lastHeaderLine + 1 >= lines.Count())
                return new ParseResult<Message>("the CRLF after the content length field is missing");

            if (header.ContentLength == 0)
                return new ParseResult<Message>(new Message(header, new NoBody()));

            var bodyParser = _bodyParserFactory.Create(header.ContentType);
            var bodyResult = bodyParser.Parse(lines, lastHeaderLine + 2, lines.Count() - 1);
            if (bodyResult.IsError)
                return bodyResult.ToParseResult<Message>();

            return new ParseResult<Message>(new Message(header, bodyResult.Result));
        }

        private ParseResult<Header.Header> ParseHeader(IReadOnlyList<string> lines, out int lastHeaderLine)
        {
            lastHeaderLine = FindLastHeaderLine(lines);

            if (lastHeaderLine < 0)
                return new ParseResult<Header.Header>("couldn't find empty line after header");

            return _headerParser.Parse(lines, lastHeaderLine);
        }

        private static int FindLastHeaderLine(IReadOnlyList<string> lines)
        {
            for (var i = 1; i < lines.Count(); ++i)
            {
                if (!string.IsNullOrEmpty(lines[i]))
                    continue;

                return i - 1;
            }

            return -1;
        }
    }
}
