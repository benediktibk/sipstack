using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SipStack.Body.Sdp;
using SipStack.Network;
using System.Net;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class ConnectionInformationTest
    {
        [TestMethod]
        public void Parse_Ipv4Unicast_AllValuesAreCorrect()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP4 10.122.69.145");
            
            connectionInformation.Result.NetType.Should().Be(NetType.Internet);
            connectionInformation.Result.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformation.Result.Host.Should().Be("10.122.69.145");
            connectionInformation.Result.IsMulticast.Should().BeFalse();
            connectionInformation.Result.IsUnicast.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv6Unicast_AllValuesAreCorrect()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP6 0015::101");
            
            connectionInformation.Result.NetType.Should().Be(NetType.Internet);
            connectionInformation.Result.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformation.Result.Host.Should().Be("0015::101");
            connectionInformation.Result.IsMulticast.Should().BeFalse();
            connectionInformation.Result.IsUnicast.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv6Multicast_AllValuesAreCorrect()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP6 FF15::101/23");
            
            connectionInformation.Result.NetType.Should().Be(NetType.Internet);
            connectionInformation.Result.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformation.Result.Host.Should().Be("FF15::101");
            connectionInformation.Result.NumberOfMulticastAddresses.Should().Be(23);
            connectionInformation.Result.IsMulticast.Should().BeTrue();
            connectionInformation.Result.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv6MulticastMultipleAddresses_AllValuesAreCorrect()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP6 FF15::101/3");
            
            connectionInformation.Result.NetType.Should().Be(NetType.Internet);
            connectionInformation.Result.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformation.Result.Host.Should().Be("FF15::101");
            connectionInformation.Result.NumberOfMulticastAddresses.Should().Be(3);
            connectionInformation.Result.IsMulticast.Should().BeTrue();
            connectionInformation.Result.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithTtl_AllValuesAreCorrect()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP4 224.2.36.42/127");
            
            connectionInformation.Result.NetType.Should().Be(NetType.Internet);
            connectionInformation.Result.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformation.Result.Host.Should().Be("224.2.36.42");
            connectionInformation.Result.MulticastTimeToLive.Should().Be(127);
            connectionInformation.Result.NumberOfMulticastAddresses.Should().Be(1);
            connectionInformation.Result.IsMulticast.Should().BeTrue();
            connectionInformation.Result.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithTtlAndMultipleAddress_AllValuesAreCorrect()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP4 224.2.36.42/127/2");
            
            connectionInformation.Result.NetType.Should().Be(NetType.Internet);
            connectionInformation.Result.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformation.Result.Host.Should().Be("224.2.36.42");
            connectionInformation.Result.MulticastTimeToLive.Should().Be(127);
            connectionInformation.Result.NumberOfMulticastAddresses.Should().Be(2);
            connectionInformation.Result.IsMulticast.Should().BeTrue();
            connectionInformation.Result.IsUnicast.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_Ipv6MulticastMultipleAddressesAndInvalidTtlCount_Error()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP6 FF15::101/3/56");

            connectionInformation.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_AddressTypeIpv6WithIpv4Address_Success()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP6 224.2.36.42");

            //will be considered as fqdn in this case
            connectionInformation.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithNegativeTtl_Error()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP4 224.2.36.42/-127");

            connectionInformation.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithZeroTtl_Error()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP4 224.2.36.42/0");

            connectionInformation.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithTtlAndZeroMultipleAddress_Error()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP4 224.2.36.42/127/0");

            connectionInformation.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithTtlAndNegativeMultipleAddress_Error()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP4 224.2.36.42/127/-2");

            connectionInformation.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithoutTtl_Success()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP4 224.2.36.42");

            //not absolutely correct, but for the sake of compatibily allowed
            connectionInformation.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Ipv4MulticastWithoutTtlButAddressCount_Error()
        {
            var connectionInformation = ConnectionInformation.Parse("IN IP4 224.2.36.42//3");

            connectionInformation.IsError.Should().BeTrue();
        }
    }
}
