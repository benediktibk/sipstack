using SipStack.Utils;

namespace SipStack.Body.Sdp
{
    public class VersionLine : ILine
    {
        public VersionLine(int version)
        {
            Version = version;
        }

        public int Version { get; }

        public static ParseResult<ILine> Parse(string data)
        {
            int version;

            if (!int.TryParse(data, out version))
                return new ParseResult<ILine>("could not parse version of SDP");

            if (version != 0)
                return new ParseResult<ILine>($"SDP version {version} is not supported");

            return new ParseResult<ILine>(new VersionLine(version));
        }
    }
}
