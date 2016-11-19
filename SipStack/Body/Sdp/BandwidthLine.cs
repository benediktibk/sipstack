using SipStack.Utils;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class BandwidthLine : ILine
    {
        public BandwidthLine(BandwidthType type, int bandwidth)
        {
            Type = type;
            Bandwidth = bandwidth;
        }

        public BandwidthType Type { get; }
        public int Bandwidth { get; }

        public static ParseResult<ILine> Parse(string data)
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
