using System;

namespace SipStack.Body.Sdp
{
    public class LineWithUsage
    {
        public LineWithUsage(ILine line)
        {
            if (line == null)
                throw new ArgumentNullException("line");

            Line = line;
            Used = false;
        }

        public ILine Line { get; private set; }
        public bool Used { get; private set; }

        public void MarkAsUsed()
        {
            if (Used)
                throw new InvalidOperationException("this line was already used");

            Used = true;
        }
    }
}
