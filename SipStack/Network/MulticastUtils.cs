using NetTools;
using System.Net;
using System;

namespace SipStack.Network
{
    public static class MulticastUtils
    {
        private static IPAddressRange _ipv4MulticastRange = IPAddressRange.Parse("224.0.0.0 - 239.255.255.255");
        private static IPAddressRange _ipv6MulticastRAnge = IPAddressRange.Parse("FF00::/8");

        public static bool IsMulticast(IPAddress ipAddress)
        {
            switch (ipAddress.AddressFamily)
            {
                case System.Net.Sockets.AddressFamily.InterNetwork: return _ipv4MulticastRange.Contains(ipAddress);
                case System.Net.Sockets.AddressFamily.InterNetworkV6: return _ipv6MulticastRAnge.Contains(ipAddress);
                default: throw new NotImplementedException();
            }
        }
    }
}
