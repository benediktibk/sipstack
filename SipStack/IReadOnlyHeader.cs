using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack
{
    public interface IReadOnlyHeader
    {
        IMethod Method { get; }
        int MaxForwards { get; }
        int CallSequenceCount { get; }
        RequestMethod CallSequenceMethod { get; }
        string CallId { get; }
        string From { get; }
        string To { get; }
        string Contact { get; }
        string Via { get; }
    }
}
