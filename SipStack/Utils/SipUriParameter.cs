using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack.Utils
{
    public class SipUriParameter
    {
        public SipUriParameter(SipUriParameterType type, string value)
        {
            Type = type;
            Value = value;
        }

        public SipUriParameterType Type { get; }
        public string Value { get; }
    }
}
