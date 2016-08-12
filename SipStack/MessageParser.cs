using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack
{
    public class MessageParser
    {
        private Header _header;
        private IBody _body;
        private IList<string> _lines;

        public MessageParser(string message)
        {
            _lines = message.Split('\n');
        }

        public Message Parse()
        {
            _header = new Header();
            _body = null;

            ParseRequestLine();

            return new Message(_header, _body);
        }

        private void ParseRequestLine()
        {
            var content = _lines[0].Split(' ');
            var request = new Request(RequestMethodUtils.Parse(content[0]), content[1]);
            _header.Method = request;
        }
    }
}
