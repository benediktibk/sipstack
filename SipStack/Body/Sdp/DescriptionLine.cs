using SipStack.Utils;

namespace SipStack.Body.Sdp
{
    public class DescriptionLine : ILine
    {
        public DescriptionLine(string description)
        {
            Description = description;
        }

        public string Description { get; }

        public static ParseResult<ILine> Parse(string data)
        {
            return new ParseResult<ILine>(new DescriptionLine(data));
        }
    }
}
