namespace SipStack
{
    public class Header : IReadOnlyHeader
    {
        public IMethod Method { get; set; }
        public int MaxForwards { get; set; }
        public int CallSequenceCount { get; set; }
        public RequestMethod CallSequenceMethod { get; set; }
        public string CallId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Contact { get; set; }
        public string Via { get; set; }
    }
}
