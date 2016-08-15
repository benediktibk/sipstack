using System;

namespace SipStack
{
    public class NoBody : IBody
    {
        public int ContentLength => 0;

        public override string ToString()
        {
            return "";
        }
    }
}
