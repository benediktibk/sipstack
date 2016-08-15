namespace SipStack
{
    public interface IBody
    {
        int ContentLength { get; }
        void AddTo(MessageBuilder messageBuilder);
    }
}
