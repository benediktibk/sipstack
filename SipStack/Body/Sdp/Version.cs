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
                return ParseResult<Version>.CreateError("could not parse version of SDP");

            if (version != 0)
                return ParseResult<Version>.CreateError($"SDP version {version} is not supported");

            return ParseResult<Version>.CreateSuccess(new Version(version));
        }
    }
}
