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
            connectionInformationLine.ConnectionInformation.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.ConnectionInformation.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformationLine.ConnectionInformation.Host.Should().Be("10.122.69.145");
            connectionInformationLine.ConnectionInformation.IsMulticast.Should().BeFalse();
            connectionInformationLine.ConnectionInformation.IsUnicast.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv6Unicast_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.Parse("IN IP6 0015::101");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.ConnectionInformation.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.ConnectionInformation.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformationLine.ConnectionInformation.Host.Should().Be("0015::101");
            connectionInformationLine.ConnectionInformation.IsMulticast.Should().BeFalse();
            connectionInformationLine.ConnectionInformation.IsUnicast.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv6Multicast_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.Parse("IN IP6 FF15::101/23");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.ConnectionInformation.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.ConnectionInformation.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformationLine.ConnectionInformation.Host.Should().Be("FF15::101");
            connectionInformationLine.ConnectionInformation.NumberOfMulticastAddresses.Should().Be(23);
            connectionInformationLine.ConnectionInformation.IsMulticast.Should().BeTrue();
            connectionInformationLine.ConnectionInformation.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv6MulticastMultipleAddresses_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.Parse("IN IP6 FF15::101/3");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.ConnectionInformation.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.ConnectionInformation.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformationLine.ConnectionInformation.Host.Should().Be("FF15::101");
            connectionInformationLine.ConnectionInformation.NumberOfMulticastAddresses.Should().Be(3);
            connectionInformationLine.ConnectionInformation.IsMulticast.Should().BeTrue();
            connectionInformationLine.ConnectionInformation.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithTtl_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42/127");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.ConnectionInformation.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.ConnectionInformation.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformationLine.ConnectionInformation.Host.Should().Be("224.2.36.42");
            connectionInformationLine.ConnectionInformation.MulticastTimeToLive.Should().Be(127);
            connectionInformationLine.ConnectionInformation.NumberOfMulticastAddresses.Should().Be(1);
            connectionInformationLine.ConnectionInformation.IsMulticast.Should().BeTrue();
            connectionInformationLine.ConnectionInformation.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithTtlAndMultipleAddress_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42/127/2");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.ConnectionInformation.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.ConnectionInformation.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformationLine.ConnectionInformation.Host.Should().Be("224.2.36.42");
            connectionInformationLine.ConnectionInformation.MulticastTimeToLive.Should().Be(127);
            connectionInformationLine.ConnectionInformation.NumberOfMulticastAddresses.Should().Be(2);
            connectionInformationLine.ConnectionInformation.IsMulticast.Should().BeTrue();
            connectionInformationLine.ConnectionInformation.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv6MulticastMultipleAddressesAndInvalidTtlCount_Error()
        {
            var line = ConnectionInformationLine.Parse("IN IP6 FF15::101/3/56");

            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_AddressTypeIpv6WithIpv4Address_Success()
        {
            var line = ConnectionInformationLine.Parse("IN IP6 224.2.36.42");

            //will be considered as fqdn in this case
            line.IsSuccess.Should().BeTrue();
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
        public void Parse_Ipv4MulticastWithoutTtl_Success()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42");
            
            //not absolutely correct, but for the sake of compatibily allowed
            line.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithoutTtlButAddressCount_Error()
        {
            var line = ConnectionInformationLine.Parse("IN IP4 224.2.36.42//3");

            line.IsError.Should().BeTrue();
        }
    }
}
