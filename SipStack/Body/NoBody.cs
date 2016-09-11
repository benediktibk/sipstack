namespace SipStack.Body
{
    public class NoBody : IBody
    {
        public int ContentLength => 0;

        public void AddTo(MessageBuilder messageBuilder)
        { }
    }
}
