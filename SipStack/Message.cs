using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack
{
    public class Message
    {
        public Message(Header header)
        {
            Header = header;
        }

        public Header Header { get; private set; }

        public override string ToString()
        {
            return "";
        }
    }
}
