using SipStack.Utils;
using System;

namespace SipStack.Body.Sdp
{
    public class SessionNameLine : ILine
    {
        private readonly string _name;

        public SessionNameLine(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentOutOfRangeException("name");

            _name = name;
        }

        public string Name => _name;

        public static ParseResult<ILine> Parse(string data)
        {
            if (string.IsNullOrEmpty(data))
                return new ParseResult<ILine>("the session name must not be empty");

            return new ParseResult<ILine>(new SessionNameLine(data));
        }
    }
}
