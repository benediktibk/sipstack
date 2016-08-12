﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack
{
    public class MessageParser
    {
        private Header _header;
        private IBody _body;
        private IList<string> _lines;
        private MessageParseError _parseError;
        private string _parseErrorMessage;

        public MessageParser(string message)
        {
            _parseError = MessageParseError.None;
            _parseErrorMessage = "";
            _lines = message.Split('\n');
        }

        public bool ErrorOccurred => _parseError != MessageParseError.None;

        public MessageParseResult Parse()
        {
            _header = new Header();
            _body = null;

            ParseRequestLine();

            if (ErrorOccurred)
                return CreateErrorResult();

            return new MessageParseResult(new Message(_header, _body));
        }

        private void ParseRequestLine()
        {
            if (_lines.Count == 0)
            {
                _parseError = MessageParseError.InvalidRequestLine;
                _parseErrorMessage = "request line is missing";
                return;
            }

            var content = _lines[0].Split(' ');
        
            if (content.Count() != 3)
            {
                _parseError = MessageParseError.InvalidRequestLine;
                _parseErrorMessage = "request line has invalid format";
                return;
            }

            if (content[2] != "SIP/2.0")
            {
                _parseError = MessageParseError.InvalidRequestLine;
                _parseErrorMessage = $"sip version {content[2]} is not supported";
                return;
            }

            RequestMethod requestMethod;
            if (!RequestMethodUtils.TryParse(content[0], out requestMethod))
            {
                _parseError = MessageParseError.InvalidRequestLine;
                _parseErrorMessage = $"invalid request {content[0]}";
                return;
            }

            var request = new Request(requestMethod, content[1]);

            _header.Method = request;
        }

        private MessageParseResult CreateErrorResult()
        {
            return new MessageParseResult(_parseError, _parseErrorMessage);
        }
    }
}
