using SipStack.Utils;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class Bandwidth
    {
        public Bandwidth(BandwidthType type, int bandwidth)
        {
            Type = type;
            Amount = bandwidth;
        }

        public BandwidthType Type { get; }
        public int Amount { get; }

        public static ParseResult<Bandwidth> Parse(string data)
        {
            var pattern = @"(.*):(.*)";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return ParseResult<Bandwidth>.CreateError($"the bandwidth definition '{data}' is malformed");

            var bandwidthTypeString = matches[0].Groups[1].Value;
            var bandwidthString = matches[0].Groups[2].Value;
            BandwidthType bandwidthType;
            int bandwidth;

            if (!BandwidthTypeUtils.TryParse(bandwidthTypeString, out bandwidthType))
                bandwidthType = BandwidthType.Unknown;

            if (!int.TryParse(bandwidthString, out bandwidth))
                return ParseResult<Bandwidth>.CreateError($"the bandwidth '{bandwidthString}' is not a valid integer");

            return ParseResult<Bandwidth>.CreateSuccess(new Bandwidth(bandwidthType, bandwidth));
        }
    }
}
