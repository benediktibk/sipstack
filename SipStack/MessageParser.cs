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
            var lines = message.Split('\n');
            var header = new Header();
            IBody body = null;

            var requestLineResult = _requestLineParser.Parse(lines[0]);
            if (requestLineResult.IsError)
                return requestLineResult.ToParseResult<Message>();

            for (var i = 1; i < lines.Count(); ++i)
            {
                var currentLine = lines[i];

                if (string.IsNullOrEmpty(currentLine) && lines.Count() > i + 1)
                {
                    var startLine = i + 1;
                    var endLine = lines.Count() - 1;
                    var bodyResult = _bodyParser.Parse(lines, startLine, endLine);
                    if (bodyResult.IsError)
                        return bodyResult.ToParseResult<Message>();

                    body = bodyResult.Result;
                    break;
                }

                var headerFieldResult = _headerFieldParser.Parse(currentLine);

                if (headerFieldResult.IsError)
                    return headerFieldResult.ToParseResult<Message>();

                // TODO: set the field in the header
            }

            return new ParseResult<Message>(new Message(header, body));
        }
    }
}
