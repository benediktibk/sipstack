using System;

namespace SipStack
{
    public class NoBody : IBody
    {
        public int ContentLength => 0;

        public string CreateHeaderInformation ()
        {
            return "Content-Length: 0\n\n";
        }

        public override string ToString()
        {
            return "";
        }
    }
}
