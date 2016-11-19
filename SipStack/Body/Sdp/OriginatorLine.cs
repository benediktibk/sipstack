﻿using SipStack.Network;
using SipStack.Utils;
using System.Net;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class OriginatorLine : ILine
    {
        public OriginatorLine(string username, long sessionId, long sessionVersion, NetType netType, AddressType addressType, IPAddress ipAddress)
        {
            Originator = new Originator(username, sessionId, sessionVersion, netType, addressType, ipAddress);
        }

        public Originator Originator { get; }

        public static ParseResult<ILine> Parse(string data)
        {
            var pattern = @"^(.*) (.*) (.*) (.*) (.*) (.*)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return new ParseResult<ILine>($"the data for originator '{data}' is malformed");

            var match = matches[0];
            string username;
            long sessionId;
            long sessionVersion;
            NetType netType;
            AddressType addressType;
            IPAddress ipAddress;

            username = match.Groups[1].Value;

            if (!long.TryParse(match.Groups[2].Value, out sessionId))
                return new ParseResult<ILine>($"the Session ID '{match.Groups[2].Value}' is not a valid integer");

            if (!long.TryParse(match.Groups[3].Value, out sessionVersion))
                return new ParseResult<ILine>($"the Session Version '{match.Groups[3].Value}' is not a valid integer");

            if (!NetTypeUtils.TryParse(match.Groups[4].Value, out netType))
                return new ParseResult<ILine>($"the net type '{match.Groups[4].Value}' is not supported");

            if (!AddressTypeUtils.TryParse(match.Groups[5].Value, out addressType))
                return new ParseResult<ILine>($"the address type '{match.Groups[5].Value}' is not supported");

            if (!IPAddress.TryParse(match.Groups[6].Value, out ipAddress))
                return new ParseResult<ILine>($"the ip address '{match.Groups[6].Value}' is invalid");

            if (!AddressTypeUtils.Match(addressType, ipAddress.AddressFamily))
                return new ParseResult<ILine>("the address type does not match type of the unicast address");

            return new ParseResult<ILine>(new OriginatorLine(username, sessionId, sessionVersion, netType, addressType, ipAddress));
        }
    }
}
