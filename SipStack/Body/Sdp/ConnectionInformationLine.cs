﻿using SipStack.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SipStack.Body.Sdp
{
    public class ConnectionInformationLine : ILine
    {
        private readonly NetType _netType;
        private readonly AddressType _addressType;
        private readonly IPAddress _ipAddress;
        private readonly int _multicastTimeToLive;
        private readonly int _numberOfMulticastAddresses;

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

        public NetType NetType => _netType;
        public AddressType AddressType => _addressType;
        public IPAddress IPAddress => _ipAddress;
        public int MulticastTimeToLive => _multicastTimeToLive;
        public int NumberOfMulticastAddresses => _numberOfMulticastAddresses;
        public bool IsMulticast => _numberOfMulticastAddresses > 0;
        public bool IsUnicast => !IsMulticast;

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

            switch (addressType)
            {
                case AddressType.Ipv4: return CreateFromIpv4(ipAddress, firstExtensionString, secondExtensionString);
                case AddressType.Ipv6: return CreateFromIpv6(ipAddress, firstExtensionString, secondExtensionString);
                default: throw new NotImplementedException();
            }
        }

        public static ParseResult<ILine> CreateFromIpv4(IPAddress ipAddress, string firstExtension, string secondExtension)
        {
            int ttlCount = 0;
            var ttlCountMissing = string.IsNullOrEmpty(firstExtension);
            int multiCastAddressCount = 1;
            var multiCastAddressCountMissing = string.IsNullOrEmpty(secondExtension);
            ILine result;

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

            if (MulticastUtils.IsMulticast(ipAddress))
                result = new ConnectionInformationLine(NetType.Internet, AddressType.Ipv4, ipAddress, multiCastAddressCount, ttlCount);
            else
            {
                if (ttlCount > 0 || multiCastAddressCount > 0)
                    return new ParseResult<ILine>("for unicast addresses the specification of TTL or address count is forbidden");

                result = new ConnectionInformationLine(NetType.Internet, AddressType.Ipv4, ipAddress);
            }

            return new ParseResult<ILine>(result);
        }

        public static ParseResult<ILine> CreateFromIpv6(IPAddress ipAddress, string firstExtension, string secondExtension)
        {
            if (!string.IsNullOrEmpty(secondExtension))
                return new ParseResult<ILine>("for IPv6 address there must be at most one extension to the ipaddress");

            int multiCastAddressCount = 1;
            var multiCastAddressCountMissing = string.IsNullOrEmpty(firstExtension);
            ILine result;

            if (!multiCastAddressCountMissing)
            {
                if (!int.TryParse(secondExtension, out multiCastAddressCount))
                    return new ParseResult<ILine>($"the value for the number of multicast addresses '{secondExtension}' is not a valid integer");

                if (multiCastAddressCount < 1)
                    return new ParseResult<ILine>($"the value for the number of multicast addresses '{multiCastAddressCount}' must be positive");
            }

            if (MulticastUtils.IsMulticast(ipAddress))
                result = new ConnectionInformationLine(NetType.Internet, AddressType.Ipv6, ipAddress, multiCastAddressCount);
            else
            {
                if ( multiCastAddressCount > 0)
                    return new ParseResult<ILine>("for unicast addresses the specification of address count is forbidden");

                result = new ConnectionInformationLine(NetType.Internet, AddressType.Ipv6, ipAddress);
            }

            return new ParseResult<ILine>(result);
        }
    }
}
