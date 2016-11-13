using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public class TimeDescription
    {
        private readonly List<RepeatLine> _repeatings;

        public TimeDescription(TimeLine time, IReadOnlyList<RepeatLine> repeatings)
        {
            Time = time;
            _repeatings = repeatings.ToList();
        }

        public TimeLine Time { get; }
        public IReadOnlyList<RepeatLine> Repeatings => _repeatings;
        public int UsedLines => 1 + _repeatings.Count();
    }
}
