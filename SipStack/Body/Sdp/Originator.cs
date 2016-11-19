using SipStack.Network;
using System.Net;

namespace SipStack.Body.Sdp
{
    public class Originator
    {
        public Originator(string username, long sessionId, long sessionVersion, NetType netType, AddressType addressType, IPAddress ipAddress)
        {
            Username = username;
            SessionId = sessionId;
            SessionVersion = sessionVersion;
            NetType = netType;
            AddressType = addressType;
            IpAddress = ipAddress;
        }

        public string Username { get; }
        public long SessionId { get; }
        public long SessionVersion { get; }
        public NetType NetType { get; }
        public AddressType AddressType { get; }
        public IPAddress IpAddress { get; }
    }
}
