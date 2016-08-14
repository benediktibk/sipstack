using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack
{
    public class MessageParser
    {
        private Header _header;
        private IBody _body;
        private IList<string> _lines;
        private ParseError _parseError;
        private string _parseErrorMessage;

        public MessageParser(string message)
        {
            _parseError = ParseError.None;
            _parseErrorMessage = "";
            _lines = message.Split('\n');
        }

        public bool ErrorOccurred => _parseError != ParseError.None;

        public ParseResult<Message> Parse()
        {
            _header = new Header();
            _body = null;

            ParseRequestLine();
            if (ErrorOccurred)
                return CreateErrorResult();

            for (var i = 1; i < _lines.Count; ++i)
            {
                var currentLine = _lines[i];

                if (string.IsNullOrEmpty(currentLine) && _lines.Count > i + 1)
                {
                    ParseBody(i + 1, _lines.Count - 1);
                    if (ErrorOccurred)
                        return CreateErrorResult();
                }

                ParseHeaderField(currentLine);
            }

            return new ParseResult<Message>(new Message(_header, _body));
        }

        private void ParseRequestLine()
        {
            if (_lines.Count == 0)
            {
                _parseError = ParseError.InvalidRequestLine;
                _parseErrorMessage = "request line is missing";
                return;
            }

            var content = _lines[0].Split(' ');
        
            if (content.Count() != 3)
            {
                _parseError = ParseError.InvalidRequestLine;
                _parseErrorMessage = "request line has invalid format";
                return;
            }

            if (content[2] != "SIP/2.0")
            {
                _parseError = ParseError.InvalidRequestLine;
                _parseErrorMessage = $"sip version {content[2]} is not supported";
                return;
            }

            RequestMethod requestMethod;
            if (!RequestMethodUtils.TryParse(content[0], out requestMethod))
            {
                _parseError = ParseError.InvalidRequestLine;
                _parseErrorMessage = $"invalid request {content[0]}";
                return;
            }

            var request = new Request(requestMethod, content[1]);

            _header.Method = request;
        }

        private void ParseHeaderField(string line)
        {
            var indexOfDelimiter = line.IndexOf(':');

            if (indexOfDelimiter <= 0)
            {
                _parseError = ParseError.InvalidHeaderField;
                _parseErrorMessage = $"invalid header field: {line}";
                return;
            }

            var fieldName = line.Substring(0, indexOfDelimiter - 1);
            var fieldValue = line.Substring(indexOfDelimiter + 1);
            
            //TODO: implement different fields
        }

        private void ParseBody(int startLine, int endLine)
        {
            _body = new NoBody();
        }

        private ParseResult<Message> CreateErrorResult()
        {
            return new ParseResult<Message>(_parseError, _parseErrorMessage);
        }
    }
}
