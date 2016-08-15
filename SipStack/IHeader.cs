namespace SipStack
{
    public interface IHeader
    {
        IMethod Method { get; }
        int ContentLength { get; }
        HeaderField this[HeaderFieldName fieldName] { get; }
        string ToString();
    }
}