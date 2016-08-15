using System.Text;

namespace SipStack
{
    public class Message
    {
        public Message(Header header, IBody body)
        {
            Header = header;
            Body = body;
        }

        public Header Header { get; private set; }
        public IBody Body { get; private set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(Header.ToString());
            stringBuilder.Append(Body.ToString());
            return stringBuilder.ToString();
        }
    }
}
