namespace SipStack
{
    public class Request : IMethod
    {
        public Request(RequestMethod type)
        {
            Type = type;
        }

        public RequestMethod Type { get; private set; }
    }
}
