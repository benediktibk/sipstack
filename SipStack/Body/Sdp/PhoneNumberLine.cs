using SipStack.Utils;

namespace SipStack.Body.Sdp
{
    public class PhoneNumberLine : ILine
    {
        public PhoneNumberLine(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public PhoneNumber PhoneNumber { get; }

        public static ParseResult<ILine> Parse(string data)
        {
            var parseResult = PhoneNumber.Parse(data);

            if (parseResult.IsError)
                return parseResult.ToParseResult<ILine>();

            return new ParseResult<ILine>(new PhoneNumberLine(parseResult.Result));
        }
    }
}
