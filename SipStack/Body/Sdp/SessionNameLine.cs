using SipStack.Utils;
using System;

namespace SipStack.Body.Sdp
{
    public class SessionNameLine : ILine
    {
        public SessionNameLine(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentOutOfRangeException("name");

            Name = name;
        }

        public string Name { get; }

        public static ParseResult<ILine> Parse(string data)
        {
            if (string.IsNullOrEmpty(data))
                return new ParseResult<ILine>("the session name must not be empty");

            return new ParseResult<ILine>(new SessionNameLine(data));
        }
    }
}
