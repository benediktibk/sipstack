using System;

namespace SipStack
{
    public class ParseResult<ResultType>
    {
        private ParseError _error;
        private string _errorMessage;
        private ResultType _message;

        public ParseResult(ResultType message)
        {
            _error = ParseError.None;
            _errorMessage = "";
            _message = message;
        }

        public ParseResult(ParseError error, string errorMessage)
        {
            if (error == ParseError.None)
                throw new ArgumentException("error", "this constructor can only be used for errors");

            _error = error;
            _errorMessage = errorMessage;
        }

        public bool IsSuccess => _error == ParseError.None;

        public bool IsError => _error != ParseError.None;

        public ResultType Message
        {
            get
            {
                if (IsError)
                    throw new InvalidOperationException("message couldn't be parsed, result is an error");

                return _message;
            }
        }

        public ParseError Error
        {
            get
            {
                if (IsSuccess)
                    throw new InvalidOperationException("message was parsed successfully");

                return _error;
            }
        }

        public string ErrorMessage
        {
            get
            {
                if (IsSuccess)
                    throw new InvalidOperationException("message was parsed successfully");

                return _errorMessage;
            }
        }
    }
}
