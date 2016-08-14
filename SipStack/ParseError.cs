namespace SipStack
{
    public enum ParseError
    {
        None,
        InvalidRequestLine,
        InvalidSipVersion,
        InvalidRequestMethod,
        WrongSipVersion,
        InvalidHeaderField
    }
}
