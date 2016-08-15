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
            IBody body = null;
            var header = new Header();

            var requestLineResult = _requestLineParser.Parse(lines[0]);
            if (requestLineResult.IsError)
                return requestLineResult.ToParseResult<Message>();

            header.Method = requestLineResult.Result;
            var contentLengthLine = -1;

            for (var i = 1; i < lines.Count() && contentLengthLine < 0; ++i)
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

                var headerFieldResult = _headerFieldParser.Parse(lines, i);

                if (headerFieldResult.IsError)
                    return headerFieldResult.ToParseResult<Message>();

                var headerField = headerFieldResult.Result;

                if (headerField.Name.isOfType(HeaderFieldType.ContentLength))
                    contentLengthLine = i;

                header[headerField.Name] = headerField;
            }

            return new ParseResult<Message>(new Message(header, body));
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
