using System;

namespace SipStack
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
