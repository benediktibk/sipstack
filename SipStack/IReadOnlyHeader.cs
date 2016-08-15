namespace SipStack
{
    public interface IReadOnlyHeader
    {
        IMethod Method { get; }
        int ContentLength { get; }
        HeaderField this[HeaderFieldName fieldName] { get; }
    }
}
