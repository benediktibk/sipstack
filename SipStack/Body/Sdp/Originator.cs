using SipStack.Network;
using System.Net;

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
    }
}
