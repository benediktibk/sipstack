using SipStack.Network;
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

        public ConnectionInformationLine(NetType netType, AddressType addressType, IPAddress ipAddress, int multicastTimeToLive, int numberOfMulticastAddresses)
        {
            _netType = netType;
            _addressType = addressType;
            _ipAddress = ipAddress;
            _multicastTimeToLive = multicastTimeToLive;
            _numberOfMulticastAddresses = numberOfMulticastAddresses;
        }

        public NetType NetType => _netType;
        public AddressType AddressType => _addressType;
        public IPAddress IPAddress => _ipAddress;
        public int MulticastTimeToLive => _multicastTimeToLive;
        public int NumberOfMulticastAddresses => _numberOfMulticastAddresses;

        public static ParseResult<ILine> CreateFrom(string data)
        {
            var pattern = @"^(.*) (.*) ([a-zA-Z\.:0-9]*)\/?([0-9]*)\/?([0-9]*)$";
            var matches = Regex.Matches(data, pattern);

            return new ParseResult<ILine>($"the connection information '{data}' is invalid");
        }
    }
}
