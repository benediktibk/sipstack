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

        private VersionLine(int version)
        {
            _version = version;
        }

        public int Version => _version;

        public static ParseResult<ILine> CreateFrom(string data)
        {
            int version;

            if (!int.TryParse(data, out version))
                return new ParseResult<ILine>("could not parse version of SDP");

            return new ParseResult<ILine>(new VersionLine(version));
        }
    }
}
