using SipStack.Network;
using SipStack.Utils;
using System.Net;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class Originator
    {
        public Originator(string username, long sessionId, long sessionVersion, NetType netType, AddressType addressType, string host)
        {
            Username = username;
            SessionId = sessionId;
            SessionVersion = sessionVersion;
            NetType = netType;
            AddressType = addressType;
            Host = host;
        }

        public string Username { get; }
        public long SessionId { get; }
        public long SessionVersion { get; }
        public NetType NetType { get; }
        public AddressType AddressType { get; }
        public string Host { get; }

        public static ParseResult<Originator> Parse(string data)
        {
            var pattern = @"^(.*) (.*) (.*) (.*) (.*) (.*)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return new ParseResult<Originator>($"the data for originator '{data}' is malformed");

            var match = matches[0];
            string username;
            long sessionId;
            long sessionVersion;
            NetType netType;
            AddressType addressType;
            string host;

            username = match.Groups[1].Value;
            host = match.Groups[6].Value;

            if (!long.TryParse(match.Groups[2].Value, out sessionId))
                return new ParseResult<Originator>($"the Session ID '{match.Groups[2].Value}' is not a valid integer");

            if (!long.TryParse(match.Groups[3].Value, out sessionVersion))
                return new ParseResult<Originator>($"the Session Version '{match.Groups[3].Value}' is not a valid integer");

            if (!NetTypeUtils.TryParse(match.Groups[4].Value, out netType))
                return new ParseResult<Originator>($"the net type '{match.Groups[4].Value}' is not supported");

            if (!AddressTypeUtils.TryParse(match.Groups[5].Value, out addressType))
                return new ParseResult<Originator>($"the address type '{match.Groups[5].Value}' is not supported");

            return new ParseResult<Originator>(new Originator(username, sessionId, sessionVersion, netType, addressType, host));
        }
    }
}
