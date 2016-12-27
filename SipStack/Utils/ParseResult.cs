using System;

namespace SipStack.Utils
{
    public class ParseResult<ResultType>
    {
        private readonly bool _isError;
        private readonly string _errorMessage;
        private readonly ResultType _result;
        private readonly bool _isEmpty;
        
        private ParseResult(ResultType result, bool isEmpty)
        {
            _isError = false;
            _errorMessage = "";
            _result = result;
            _isEmpty = isEmpty;
        }

        private ParseResult(string errorMessage)
        {
            if (errorMessage == null)
                throw new ArgumentException("error message is null");

            _isError = true;
            _errorMessage = errorMessage;
            _isEmpty = false;
        }

        public bool IsSuccess => !_isError;

        public bool IsError => _isError;

        public bool IsEmpty => _isEmpty;

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
            return new ParseResult<ResultType>(result, false);
        }

        public static ParseResult<ResultType> CreateEmptySuccess()
        {
            return new ParseResult<ResultType>(default(ResultType), true);
        }

        public ParseResult<TargetResultType> ToParseResult<TargetResultType>()
        {
            if (IsSuccess)
                throw new InvalidOperationException("this operation is only on an error result allowed");

            return new ParseResult<TargetResultType>(_errorMessage);
        }
    }
}
