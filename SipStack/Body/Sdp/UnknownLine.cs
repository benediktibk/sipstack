using SipStack.Utils;
using System;

namespace SipStack.Body.Sdp
{
    public class UnknownLine : ILine
    {
        public static ParseResult<ILine> Parse(string data)
        {
            throw new NotImplementedException();
        }
    }
}
