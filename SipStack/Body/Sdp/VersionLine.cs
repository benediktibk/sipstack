using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack.Body.Sdp
{
    public class VersionLine : ILine
    {
        private readonly int _version;

        public VersionLine(int version)
        {
            _version = version;
        }

        public int Version => _version;

        public static ParseResult<ILine> CreateFrom(string data)
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
