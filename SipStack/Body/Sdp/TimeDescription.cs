using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public class TimeDescription
    {
        public TimeDescription(Timing time, IEnumerable<Repeat> repeatings)
        {
            if (time == null)
                throw new ArgumentNullException("time");
            if (repeatings == null)
                throw new ArgumentNullException("repeatings");

            Time = time;
            Repeatings = repeatings.ToList();
        }

        public Timing Time { get; }
        public IReadOnlyList<Repeat> Repeatings { get; }
    }
}
