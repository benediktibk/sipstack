using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack.Body.Sdp
{
    public class Repeat
    {
        public Repeat(TimeSpan repeatInterval, TimeSpan activeDuration, TimeSpan offsetStart, TimeSpan offsetEnd)
        {
            if (TimeSpan.Compare(offsetStart, offsetEnd) > 0)
                throw new ArgumentException("start must be before end");

            RepeatInterval = repeatInterval;
            ActiveDuration = activeDuration;
            OffsetStart = offsetStart;
            OffsetEnd = offsetEnd;
        }

        public TimeSpan RepeatInterval { get; }
        public TimeSpan ActiveDuration { get; }
        public TimeSpan OffsetStart { get; }
        public TimeSpan OffsetEnd { get; }
    }
}
