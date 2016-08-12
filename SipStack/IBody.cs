namespace SipStack
{
    public interface IBody
    {
        int ContentLength { get; }
        string CreateHeaderInformation();
        string ToString();
    }
}
