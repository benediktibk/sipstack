using SipStack.Utils;
using System;

namespace SipStack.Body.Sdp
{
    public class EmailAddressLine : ILine
    {
        private readonly EmailAddress _emailAddress;

        public EmailAddressLine(EmailAddress emailAddress)
        {
            _emailAddress = emailAddress;
        }

        public EmailAddress EmailAddress => _emailAddress;

        public static ParseResult<ILine> Parse(string data)
        {
            var parseResult = EmailAddress.Parse(data);

            if (parseResult.IsError)
                return parseResult.ToParseResult<ILine>();

            return new ParseResult<ILine>(new EmailAddressLine(parseResult.Result));
        }
    }
}
