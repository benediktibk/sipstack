using SipStack.Network;
using System;
using System.Net;

namespace SipStack.Body.Sdp
{
    public class ConnectionInformation
    {
        #region private variables

        private readonly int _multicastTimeToLive;
        private readonly int _numberOfMulticastAddresses;

        #endregion

        #region constructors

        public ConnectionInformation(NetType netType, AddressType addressType, IPAddress ipAddress, int numberOfMulticastAddresses, int multicastTimeToLive)
        {
            if (multicastTimeToLive < 1)
                throw new ArgumentOutOfRangeException("multicastTimeToLive");
            if (numberOfMulticastAddresses < 1)
                throw new ArgumentOutOfRangeException("numberOfMulticastAddresses");
            if (ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork ||
                !MulticastUtils.IsMulticast(ipAddress))
                throw new ArgumentOutOfRangeException("ipAddress", "this constructor can be used only for multicast IPv4 addresses");
            if (!AddressTypeUtils.Match(addressType, ipAddress.AddressFamily))
                throw new ArgumentOutOfRangeException("ipAddress", "the specified address family does not match the ip address");

            NetType = netType;
            AddressType = addressType;
            IpAddress = ipAddress;
            _multicastTimeToLive = multicastTimeToLive;
            _numberOfMulticastAddresses = numberOfMulticastAddresses;
        }

        public ConnectionInformation(NetType netType, AddressType addressType, IPAddress ipAddress, int numberOfMulticastAddresses)
        {
            if (numberOfMulticastAddresses < 1)
                throw new ArgumentOutOfRangeException("numberOfMulticastAddresses");
            if (ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6 ||
                !MulticastUtils.IsMulticast(ipAddress))
                throw new ArgumentOutOfRangeException("ipAddress", "this constructor can be used only for multicast IPv6 addresses");
            if (!AddressTypeUtils.Match(addressType, ipAddress.AddressFamily))
                throw new ArgumentOutOfRangeException("ipAddress", "the specified address family does not match the ip address");

            NetType = netType;
            AddressType = addressType;
            IpAddress = ipAddress;
            _multicastTimeToLive = 0;
            _numberOfMulticastAddresses = numberOfMulticastAddresses;
        }

        public ConnectionInformation(NetType netType, AddressType addressType, IPAddress ipAddress)
        {
            if ((ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork && ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6) ||
                MulticastUtils.IsMulticast(ipAddress))
                throw new ArgumentOutOfRangeException("ipAddress", "this constructor can be used only for unicast IPv4 and IPv6 addresses");
            if (!AddressTypeUtils.Match(addressType, ipAddress.AddressFamily))
                throw new ArgumentOutOfRangeException("ipAddress", "the specified address family does not match the ip address");

            NetType = netType;
            AddressType = addressType;
            IpAddress = ipAddress;
            _multicastTimeToLive = 0;
            _numberOfMulticastAddresses = 0;
        }

        #endregion

        #region properties

        public NetType NetType { get; }
        public AddressType AddressType { get; }
        public IPAddress IpAddress { get; }
        public int MulticastTimeToLive
        {
            get
            {
                if (IsUnicast || AddressType == AddressType.Ipv6)
                    throw new InvalidOperationException();

                return _multicastTimeToLive;
            }
        }
        public int NumberOfMulticastAddresses
        {
            get
            {
                if (IsUnicast)
                    throw new InvalidOperationException();

                return _numberOfMulticastAddresses;
            }
        }
        public bool IsMulticast => _numberOfMulticastAddresses > 0;
        public bool IsUnicast => !IsMulticast;

        #endregion
    }
}
