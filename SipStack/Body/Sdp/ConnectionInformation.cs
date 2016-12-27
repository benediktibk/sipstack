using SipStack.Network;
using SipStack.Utils;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class ConnectionInformation
    {
        #region private variables

        private readonly int _multicastTimeToLive;
        private readonly int _numberOfMulticastAddresses;

        #endregion

        #region constructors

        public ConnectionInformation(NetType netType, AddressType addressType, string host, int numberOfMulticastAddresses, int multicastTimeToLive) :
            this(netType, addressType, host, numberOfMulticastAddresses, multicastTimeToLive, true)
        {
            if (multicastTimeToLive < 1)
                throw new ArgumentOutOfRangeException("multicastTimeToLive");
            if (numberOfMulticastAddresses < 1)
                throw new ArgumentOutOfRangeException("numberOfMulticastAddresses");
            if (addressType == AddressType.Ipv6)
                throw new ArgumentException("multicastTimeToLive", "for IPv6 multicast it is forbidden to specify a TTL");
        }

        public ConnectionInformation(NetType netType, AddressType addressType, string host, int numberOfMulticastAddresses) :
            this(netType, addressType, host, numberOfMulticastAddresses, 0, true)
        {
            if (numberOfMulticastAddresses < 1)
                throw new ArgumentOutOfRangeException("numberOfMulticastAddresses");
            if (addressType == AddressType.Ipv4)
                throw new ArgumentException("addressType", "for IPv4 multicast a TTL must be specified");
        }

        public ConnectionInformation(NetType netType, AddressType addressType, string host) :
            this(netType, addressType, host, 0, 0, true)
        { }

        private ConnectionInformation(NetType netType, AddressType addressType, string host, int numberOfMulticastAddresses, int multicastTimeToLive, bool isPrivateVersion)
        {
            NetType = netType;
            AddressType = addressType;
            Host = host;
            _multicastTimeToLive = multicastTimeToLive;
            _numberOfMulticastAddresses = numberOfMulticastAddresses;
        }

        #endregion

        #region properties

        public NetType NetType { get; }
        public AddressType AddressType { get; }
        public string Host { get; }
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

        public static ParseResult<ConnectionInformation> Parse(string data)
        {
            var pattern = @"^(.*) (.*) ([a-zA-Z\.:0-9]*)\/?([0-9]*)\/?([0-9]*)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return new ParseResult<ConnectionInformation>($"the connection information '{data}' is invalid");

            var match = matches[0];
            var netTypeString = match.Groups[1].Value;
            var addressTypeString = match.Groups[2].Value;
            var ipAddressString = match.Groups[3].Value;
            var firstExtensionString = match.Groups[4].Value;
            var secondExtensionString = match.Groups[5].Value;

            NetType netType;
            AddressType addressType;
            string host = ipAddressString;

            if (!NetTypeUtils.TryParse(netTypeString, out netType))
                return new ParseResult<ConnectionInformation>($"invalid net type '{netTypeString}' for connection information");

            if (!AddressTypeUtils.TryParse(addressTypeString, out addressType))
                return new ParseResult<ConnectionInformation>($"invalid address type '{addressTypeString}' for connection information");


            if (!string.IsNullOrEmpty(firstExtensionString))
            {
                switch (addressType)
                {
                    case AddressType.Ipv4:
                        return ParseFromMulticastIpv4(host, firstExtensionString, secondExtensionString);
                    case AddressType.Ipv6:
                        return ParseFromMulticastIpv6(host, firstExtensionString, secondExtensionString);
                    default: throw new NotImplementedException();
                }
            }
            else
                return ParseFromUnicast(addressType, host, firstExtensionString, secondExtensionString);
        }

        private static ParseResult<ConnectionInformation> ParseFromUnicast(AddressType addressType, string host, string firstExtension, string secondExtension)
        {
            if (!string.IsNullOrEmpty(firstExtension) || !string.IsNullOrEmpty(secondExtension))
                return new ParseResult<ConnectionInformation>("for unicast addresses the specification of TTL or address count is forbidden");

            return new ParseResult<ConnectionInformation>(new ConnectionInformation(NetType.Internet, addressType, host));
        }

        private static ParseResult<ConnectionInformation> ParseFromMulticastIpv4(string host, string firstExtension, string secondExtension)
        {
            int ttlCount = 0;
            var ttlCountMissing = string.IsNullOrEmpty(firstExtension);
            int multiCastAddressCount = 1;
            var multiCastAddressCountMissing = string.IsNullOrEmpty(secondExtension);

            if (ttlCountMissing)
                return new ParseResult<ConnectionInformation>("for IPv4 multicast addresses the TTL hop count must be specified");

            if (!int.TryParse(firstExtension, out ttlCount))
                return new ParseResult<ConnectionInformation>($"the value for the TTL hop count '{firstExtension}' is not a valid integer");

            if (ttlCount < 1)
                return new ParseResult<ConnectionInformation>($"the value for the TTL hop count '{ttlCount}' must be positive");

            if (!multiCastAddressCountMissing)
            {
                if (!int.TryParse(secondExtension, out multiCastAddressCount))
                    return new ParseResult<ConnectionInformation>($"the value for the number of multicast addresses '{secondExtension}' is not a valid integer");

                if (multiCastAddressCount < 1)
                    return new ParseResult<ConnectionInformation>($"the value for the number of multicast addresses '{multiCastAddressCount}' must be positive");
            }

            return new ParseResult<ConnectionInformation>(new ConnectionInformation(NetType.Internet, AddressType.Ipv4, host, multiCastAddressCount, ttlCount));
        }

        private static ParseResult<ConnectionInformation> ParseFromMulticastIpv6(string host, string firstExtension, string secondExtension)
        {
            if (!string.IsNullOrEmpty(secondExtension))
                return new ParseResult<ConnectionInformation>("for IPv6 address there must be at most one extension to the ipaddress");

            int multiCastAddressCount = 1;
            var multiCastAddressCountMissing = string.IsNullOrEmpty(firstExtension);

            if (!multiCastAddressCountMissing)
            {
                if (!int.TryParse(firstExtension, out multiCastAddressCount))
                    return new ParseResult<ConnectionInformation>($"the value for the number of multicast addresses '{firstExtension}' is not a valid integer");

                if (multiCastAddressCount < 1)
                    return new ParseResult<ConnectionInformation>($"the value for the number of multicast addresses '{multiCastAddressCount}' must be positive");
            }

            return new ParseResult<ConnectionInformation>(new ConnectionInformation(NetType.Internet, AddressType.Ipv6, host, multiCastAddressCount));
        }

        #endregion
    }
}
