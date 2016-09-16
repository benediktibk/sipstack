namespace SipStack.Body.Sdp
{
    public class DescriptionLine : ILine
    {
        private readonly string _description;

        public DescriptionLine(string description)
        {
            _description = description;
        }

        public static ParseResult<ILine> CreateFrom(string data)
        {
            return new ParseResult<ILine>(new DescriptionLine(data));
        }
    }
}
