using System;

namespace SipStack.Utils
{
    public class ParseResult<ResultType>
    {
        private readonly bool _isError;
        private readonly string _errorMessage;
        private readonly ResultType _result;

        public ParseResult(ResultType result)
        {
            _isError = false;
            _errorMessage = "";
            _result = result;
        }

        // only necessary if ResultType is string
        public ParseResult(ResultType result, bool isSuccess) : 
            this(result)
        {
            if (!isSuccess)
                throw new ArgumentOutOfRangeException("isSuccess", "must be true");
        }

        public ParseResult(string errorMessage)
        {
            _isError = true;
            _errorMessage = errorMessage;
        }

        public bool IsSuccess => !_isError;

        public bool IsError => _isError;

        public ResultType Result
        {
            get
            {
                if (IsError)
                    throw new InvalidOperationException($"message couldn't be parsed, result is an error: {ErrorMessage}");

                return _result;
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

        public ParseResult<TargetResultType> ToParseResult<TargetResultType>()
        {
            return new ParseResult<TargetResultType>(_errorMessage);
        }
    }
}
