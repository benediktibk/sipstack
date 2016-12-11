using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack.Body.Sdp
{
    public class Timing
    {
        public Timing(DateTime start, DateTime end)
        {
            if (DateTime.Compare(start, end) >= 0)
                throw new ArgumentException("end must be after start");

            Start = start;
            End = end;
        }

        public DateTime Start { get; }
        public DateTime End { get; }
    }
}
