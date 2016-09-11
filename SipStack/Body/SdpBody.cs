using System;

namespace SipStack.Body
{
    public class SdpBody : IBody
    {
        public int ContentLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void AddTo(MessageBuilder messageBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
