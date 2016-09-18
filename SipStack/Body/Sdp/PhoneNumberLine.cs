using SipStack.Utils;

namespace SipStack.Body.Sdp
{
    public class PhoneNumberLine : ILine
    {
        private readonly PhoneNumber _phoneNumber;

        public PhoneNumberLine(PhoneNumber phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public PhoneNumber PhoneNumber => _phoneNumber;

        public static ParseResult<ILine> Parse(string data)
        {
            var parseResult = PhoneNumber.Parse(data);

            if (parseResult.IsError)
                return parseResult.ToParseResult<ILine>();

            return new ParseResult<ILine>(new PhoneNumberLine(parseResult.Result));
        }
    }
}
