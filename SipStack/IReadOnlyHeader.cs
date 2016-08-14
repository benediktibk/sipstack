namespace SipStack
{
    public interface IReadOnlyHeader
    {
        IMethod Method { get; }
        int ContentLength { get; }
    }
}
