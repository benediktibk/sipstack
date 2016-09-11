using System;
namespace SipStack
{
    public class BodyParserFactory
    {
        public IBodyParser Create(string contentType)
        {
            if (contentType == "application/sdp")
                return new SdpBodyParser();
            else if (string.IsNullOrEmpty(contentType))
                return new NoBodyParser();

            throw new NotImplementedException("this content type is not supported");
        }
    }
}
