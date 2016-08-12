﻿using System;

namespace SipStack
{
    public class MessageParseResult
    {
        private MessageParseError _error;
        private string _errorMessage;
        private Message _message;

        public MessageParseResult(Message message)
        {
            _error = MessageParseError.None;
            _errorMessage = "";
            _message = message;
        }

        public MessageParseResult(MessageParseError error, string errorMessage)
        {
            _error = error;
            _errorMessage = errorMessage;
            _message = null;
        }

        public bool IsSuccess => _error == MessageParseError.None;

        public bool IsError => _error != MessageParseError.None;

        public Message Message
        {
            get
            {
                if (IsError)
                    throw new InvalidOperationException("message couldn't be parsed, result is an error");

                return _message;
            }
        }

        public MessageParseError Error => _error;

        public string ErrorMessage => _errorMessage;
    }
}
