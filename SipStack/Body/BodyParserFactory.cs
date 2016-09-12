using SipStack.Body.Sdp;
using System;

namespace SipStack.Body
{
    public class BodyParserFactory
    {
        public IBodyParser Create(string contentType)
        {
            contentType = contentType?.ToLower();

            if (contentType == "application/sdp")
                return new SdpBodyParser(new LineParser());
            else if (string.IsNullOrEmpty(contentType))
                return new NoBodyParser();

            throw new NotImplementedException("this content type is not supported");
        }
    }
}
