namespace SipStack
{
    public class Request : IMethod
    {
        public Request(RequestMethod type, string uri)
        {
            Type = type;
            Uri = uri;
        }

        public RequestMethod Type { get; private set; }
        public string Uri { get; private set; }
    }
}
