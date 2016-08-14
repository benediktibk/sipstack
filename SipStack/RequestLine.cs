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
    }
}
