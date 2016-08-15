using System.Text;

namespace SipStack
{
    public class RequestLine : IMethod
    {
        public RequestLine(RequestMethod type, string uri)
        {
            Type = type;
            Uri = uri;
        }

        public RequestMethod Type { get; private set; }
        public string Uri { get; private set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0} {1} SIP/2.0", Type.ToFriendlyString(), Uri);
            return stringBuilder.ToString();
        }
    }
}
