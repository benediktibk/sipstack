using System;

namespace SipStack.Utils
{
    public class ParseResult<ResultType>
    {
        private readonly bool _isError;
        private readonly string _errorMessage;
        private readonly ResultType _result;
        
        private ParseResult(ResultType result)
        {
            _isError = false;
            _errorMessage = "";
            _result = result;
        }

        private ParseResult(string errorMessage)
        {
            if (errorMessage == null)
                throw new ArgumentException("error message is null");

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

        public static ParseResult<ResultType> CreateError(string errorMessage)
        {
            return new ParseResult<ResultType>(errorMessage);
        }

        public static ParseResult<ResultType> CreateSuccess(ResultType result)
        {
            return new ParseResult<ResultType>(result);
        }

        public static ParseResult<ResultType> CreateEmptySuccess()
        {
            return new ParseResult<ResultType>(default(ResultType));
        }

        public ParseResult<TargetResultType> ToParseResult<TargetResultType>()
        {
            if (IsSuccess)
                throw new InvalidOperationException("this operation is only on an error result allowed");

            return new ParseResult<TargetResultType>(_errorMessage);
        }
    }
}
