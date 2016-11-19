using SipStack.Utils;
using System;

namespace SipStack.Body.Sdp
{
    public class EmailAddressLine : ILine
    {
        public EmailAddressLine(EmailAddress emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public EmailAddress EmailAddress { get; }

        public static ParseResult<ILine> Parse(string data)
        {
            var parseResult = EmailAddress.Parse(data);

            if (parseResult.IsError)
                return parseResult.ToParseResult<ILine>();

            return new ParseResult<ILine>(new EmailAddressLine(parseResult.Result));
        }
    }
}
