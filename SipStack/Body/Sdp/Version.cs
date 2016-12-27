using SipStack.Utils;

namespace SipStack.Body.Sdp
{
    public class Version 
    {
        public Version(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static ParseResult<Version> Parse(string data)
        {
            int version;

            if (!int.TryParse(data, out version))
                return new ParseResult<Version>("could not parse version of SDP");

            if (version != 0)
                return new ParseResult<Version>($"SDP version {version} is not supported");

            return new ParseResult<Version>(new Version(version));
        }
    }
}
