using SipStack.Utils;
using System;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class BandwidthLine : ILine
    {
        private readonly BandwidthType _type;
        private readonly int _bandwidth;

        public BandwidthLine(BandwidthType type, int bandwidth)
        {
            _type = type;
            _bandwidth = bandwidth;
        }

        public BandwidthType Type => _type;
        public int Bandwidth => _bandwidth;

        public static ParseResult<ILine> CreateFrom(string data)
        {
            var pattern = @"(.*):(.*)";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return new ParseResult<ILine>($"the bandwidth definition '{data}' is malformed");

            var bandwidthTypeString = matches[0].Groups[1].Value;
            var bandwidthString = matches[0].Groups[2].Value;
            BandwidthType bandwidthType;
            int bandwidth;

            if (!BandwidthTypeUtils.TryParse(bandwidthTypeString, out bandwidthType))
                bandwidthType = BandwidthType.Unknown;

            if (!int.TryParse(bandwidthString, out bandwidth))
                return new ParseResult<ILine>($"the bandwidth '{bandwidthString}' is not a valid integer");

            return new ParseResult<ILine>(new BandwidthLine(bandwidthType, bandwidth));
        }
    }
}
