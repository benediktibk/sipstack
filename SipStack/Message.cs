using System.Text;

namespace SipStack
{
    public class Message
    {
        private Header _header;
        private IBody _body;

        public Message(Header header, IBody body)
        {
            _header = header;
            _body = body;
        }

        public IReadOnlyHeader Header => _header;

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(_header.ToString());
            stringBuilder.Append(_body.CreateHeaderInformation());
            stringBuilder.Append(_body.ToString());
            return stringBuilder.ToString();
        }
    }
}
