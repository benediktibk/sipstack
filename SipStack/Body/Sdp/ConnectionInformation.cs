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

        public ConnectionInformation(NetType netType, AddressType addressType, string host, int numberOfMulticastAddresses, int multicastTimeToLive) :
            this(netType, addressType, host, numberOfMulticastAddresses, multicastTimeToLive, true)
        {
            if (multicastTimeToLive < 1)
                throw new ArgumentOutOfRangeException("multicastTimeToLive");
            if (numberOfMulticastAddresses < 1)
                throw new ArgumentOutOfRangeException("numberOfMulticastAddresses");
        }

        public ConnectionInformation(NetType netType, AddressType addressType, string host, int numberOfMulticastAddresses) :
            this(netType, addressType, host, numberOfMulticastAddresses, 0, true)
        {
            if (numberOfMulticastAddresses < 1)
                throw new ArgumentOutOfRangeException("numberOfMulticastAddresses");
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
    }
}
