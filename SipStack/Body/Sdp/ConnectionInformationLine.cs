using SipStack.Network;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class ConnectionInformationLine : ILine
    {
        #region private variables

        private readonly NetType _netType;
        private readonly AddressType _addressType;
        private readonly IPAddress _ipAddress;
        private readonly int _multicastTimeToLive;
        private readonly int _numberOfMulticastAddresses;

        #endregion

        #region constructors

        public ConnectionInformationLine(NetType netType, AddressType addressType, IPAddress ipAddress, int numberOfMulticastAddresses, int multicastTimeToLive)
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

            _netType = netType;
            _addressType = addressType;
            _ipAddress = ipAddress;
            _multicastTimeToLive = multicastTimeToLive;
            _numberOfMulticastAddresses = numberOfMulticastAddresses;
        }

        public ConnectionInformationLine(NetType netType, AddressType addressType, IPAddress ipAddress, int numberOfMulticastAddresses)
        {
            if (numberOfMulticastAddresses < 1)
                throw new ArgumentOutOfRangeException("numberOfMulticastAddresses");
            if (ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6 || 
                !MulticastUtils.IsMulticast(ipAddress))
                throw new ArgumentOutOfRangeException("ipAddress", "this constructor can be used only for multicast IPv6 addresses");
            if (!AddressTypeUtils.Match(addressType, ipAddress.AddressFamily))
                throw new ArgumentOutOfRangeException("ipAddress", "the specified address family does not match the ip address");

            _netType = netType;
            _addressType = addressType;
            _ipAddress = ipAddress;
            _multicastTimeToLive = 0;
            _numberOfMulticastAddresses = numberOfMulticastAddresses;
        }

        public ConnectionInformationLine(NetType netType, AddressType addressType, IPAddress ipAddress)
        {
            if ((ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork && ipAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6) ||
                MulticastUtils.IsMulticast(ipAddress))
                throw new ArgumentOutOfRangeException("ipAddress", "this constructor can be used only for unicast IPv4 and IPv6 addresses");
            if (!AddressTypeUtils.Match(addressType, ipAddress.AddressFamily))
                throw new ArgumentOutOfRangeException("ipAddress", "the specified address family does not match the ip address");

            _netType = netType;
            _addressType = addressType;
            _ipAddress = ipAddress;
            _multicastTimeToLive = 0;
            _numberOfMulticastAddresses = 0;
        }

        #endregion

        #region properties

        public NetType NetType => _netType;
        public AddressType AddressType => _addressType;
        public IPAddress IpAddress => _ipAddress;
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

        #region static functions

        public static ParseResult<ILine> CreateFrom(string data)
        {
            var pattern = @"^(.*) (.*) ([a-zA-Z\.:0-9]*)\/?([0-9]*)\/?([0-9]*)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return new ParseResult<ILine>($"the connection information '{data}' is invalid");

            var match = matches[0];
            var netTypeString = match.Groups[1].Value;
            var addressTypeString = match.Groups[2].Value;
            var ipAddressString = match.Groups[3].Value;
            var firstExtensionString = match.Groups[4].Value;
            var secondExtensionString = match.Groups[5].Value;

            NetType netType;
            AddressType addressType;
            IPAddress ipAddress;

            if (!NetTypeUtils.TryParse(netTypeString, out netType))
                return new ParseResult<ILine>($"invalid net type '{netTypeString}' for connection information");

            if (!AddressTypeUtils.TryParse(addressTypeString, out addressType))
                return new ParseResult<ILine>($"invalid address type '{addressTypeString}' for connection information");

            if (!IPAddress.TryParse(ipAddressString, out ipAddress))
                return new ParseResult<ILine>($"invalid IP address '{ipAddressString}' for connection information");

            if (!AddressTypeUtils.Match(addressType, ipAddress.AddressFamily))
                return new ParseResult<ILine>($"the specified address family '{addressTypeString}' does not match the type of the ip address '{ipAddressString}'");

            if (MulticastUtils.IsMulticast(ipAddress))
            {
                switch (addressType)
                {
                    case AddressType.Ipv4: return CreateFromIpv4(ipAddress, firstExtensionString, secondExtensionString);
                    case AddressType.Ipv6: return CreateFromIpv6(ipAddress, firstExtensionString, secondExtensionString);
                    default: throw new NotImplementedException();
                }
            }
            else
                return CreateFromUnicast(addressType, ipAddress, firstExtensionString, secondExtensionString);
        }

        private static ParseResult<ILine> CreateFromUnicast(AddressType addressType, IPAddress ipAddress, string firstExtension, string secondExtension)
        {
            if (!string.IsNullOrEmpty(firstExtension) || !string.IsNullOrEmpty(secondExtension))
                return new ParseResult<ILine>("for unicast addresses the specification of TTL or address count is forbidden");

            return new ParseResult<ILine>(new ConnectionInformationLine(NetType.Internet, addressType, ipAddress));
        }

        private static ParseResult<ILine> CreateFromIpv4(IPAddress ipAddress, string firstExtension, string secondExtension)
        {
            int ttlCount = 0;
            var ttlCountMissing = string.IsNullOrEmpty(firstExtension);
            int multiCastAddressCount = 1;
            var multiCastAddressCountMissing = string.IsNullOrEmpty(secondExtension);

            if (ttlCountMissing)
                return new ParseResult<ILine>("for IPv4 multicast addresses the TTL hop count must be specified");

            if (!int.TryParse(firstExtension, out ttlCount))
                return new ParseResult<ILine>($"the value for the TTL hop count '{firstExtension}' is not a valid integer");

            if (ttlCount < 1)
                return new ParseResult<ILine>($"the value for the TTL hop count '{ttlCount}' must be positive");

            if (!multiCastAddressCountMissing)
            {
                if (!int.TryParse(secondExtension, out multiCastAddressCount))
                    return new ParseResult<ILine>($"the value for the number of multicast addresses '{secondExtension}' is not a valid integer");

                if (multiCastAddressCount < 1)
                    return new ParseResult<ILine>($"the value for the number of multicast addresses '{multiCastAddressCount}' must be positive");
            }

            return new ParseResult<ILine>(new ConnectionInformationLine(NetType.Internet, AddressType.Ipv4, ipAddress, multiCastAddressCount, ttlCount));
        }

        private static ParseResult<ILine> CreateFromIpv6(IPAddress ipAddress, string firstExtension, string secondExtension)
        {
            if (!string.IsNullOrEmpty(secondExtension))
                return new ParseResult<ILine>("for IPv6 address there must be at most one extension to the ipaddress");

            int multiCastAddressCount = 1;
            var multiCastAddressCountMissing = string.IsNullOrEmpty(firstExtension);

            if (!multiCastAddressCountMissing)
            {
                if (!int.TryParse(firstExtension, out multiCastAddressCount))
                    return new ParseResult<ILine>($"the value for the number of multicast addresses '{firstExtension}' is not a valid integer");

                if (multiCastAddressCount < 1)
                    return new ParseResult<ILine>($"the value for the number of multicast addresses '{multiCastAddressCount}' must be positive");
            }

            return new ParseResult<ILine>(new ConnectionInformationLine(NetType.Internet, AddressType.Ipv6, ipAddress, multiCastAddressCount));
        }

        #endregion
    }
}