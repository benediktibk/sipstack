using System;

namespace SipStack
{
    public class NoBody : IBody
    {
        public int ContentLength => 0;

        public void AddTo(MessageBuilder messageBuilder)
        { }
    }
}
