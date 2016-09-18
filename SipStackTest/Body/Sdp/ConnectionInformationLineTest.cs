using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SipStack.Body.Sdp;
using SipStack.Network;
using System.Net;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class ConnectionInformationLineTest
    {
        [TestMethod]
        public void Parse_Ipv4Unicast_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 10.122.69.145");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformationLine.IpAddress.Should().Be(IPAddress.Parse("10.122.69.145"));
            connectionInformationLine.IsMulticast.Should().BeFalse();
            connectionInformationLine.IsUnicast.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv6Unicast_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.Parse("IN IP6 0015::101");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformationLine.IpAddress.Should().Be(IPAddress.Parse("0015::101"));
            connectionInformationLine.IsMulticast.Should().BeFalse();
            connectionInformationLine.IsUnicast.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv6Multicast_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.Parse("IN IP6 FF15::101");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformationLine.IpAddress.Should().Be(IPAddress.Parse("FF15::101"));
            connectionInformationLine.NumberOfMulticastAddresses.Should().Be(1);
            connectionInformationLine.IsMulticast.Should().BeTrue();
            connectionInformationLine.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv6MulticastMultipleAddresses_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.Parse("IN IP6 FF15::101/3");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformationLine.IpAddress.Should().Be(IPAddress.Parse("FF15::101"));
            connectionInformationLine.NumberOfMulticastAddresses.Should().Be(3);
            connectionInformationLine.IsMulticast.Should().BeTrue();
            connectionInformationLine.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithTtl_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42/127");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformationLine.IpAddress.Should().Be(IPAddress.Parse("224.2.36.42"));
            connectionInformationLine.MulticastTimeToLive.Should().Be(127);
            connectionInformationLine.NumberOfMulticastAddresses.Should().Be(1);
            connectionInformationLine.IsMulticast.Should().BeTrue();
            connectionInformationLine.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithTtlAndMultipleAddress_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42/127/2");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformationLine.IpAddress.Should().Be(IPAddress.Parse("224.2.36.42"));
            connectionInformationLine.MulticastTimeToLive.Should().Be(127);
            connectionInformationLine.NumberOfMulticastAddresses.Should().Be(2);
            connectionInformationLine.IsMulticast.Should().BeTrue();
            connectionInformationLine.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv6MulticastMultipleAddressesAndInvalidTtlCount_Error()
        {
            var line = ConnectionInformationLine.Parse("IN IP6 FF15::101/3/56");

            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_AddressTypeIpv6WithIpv4Address_Error()
        {
            var line = ConnectionInformationLine.Parse("IN IP6 224.2.36.42");

            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithNegativeTtl_Error()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42/-127");
            
            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithZeroTtl_Error()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42/0");
            
            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithTtlAndZeroMultipleAddress_Error()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42/127/0");

            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithTtlAndNegativeMultipleAddress_Error()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42/127/-2");

            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithoutTtl_Error()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42");

            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithoutTtlButAddressCount_Error()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42//3");

            line.IsError.Should().BeTrue();
        }
    }
}
