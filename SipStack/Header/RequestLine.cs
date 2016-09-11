using System.Text;

namespace SipStack.Header
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

        public void AddTo(MessageBuilder messageBuilder)
        {
            messageBuilder.AddLineFormat("{0} {1} SIP/2.0", Type.ToFriendlyString(), Uri);
        }
    }
}
