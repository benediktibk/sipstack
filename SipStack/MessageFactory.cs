using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack
{
    public static class MessageFactory
    {
        public static Message Parse(string message)
        {
            var header = new Header();
            return new Message(header);
        }
    }
}
