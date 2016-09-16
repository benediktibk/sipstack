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
        public void CreateFrom_Ipv4Unicast_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.CreateFrom("IN IP4 10.122.69.145");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformationLine.IPAddress.Should().Be(IPAddress.Parse("10.122.69.145"));
            connectionInformationLine.MulticastTimeToLive.Should().Be(0);
            connectionInformationLine.NumberOfMulticastAddresses.Should().Be(0);
        }

        [TestMethod]
        public void CreateFrom_Ipv6Unicast_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.CreateFrom("IN IP6 0015::101");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformationLine.IPAddress.Should().Be(IPAddress.Parse("0015::101"));
            connectionInformationLine.MulticastTimeToLive.Should().Be(0);
            connectionInformationLine.NumberOfMulticastAddresses.Should().Be(0);
        }

        [TestMethod]
        public void CreateFrom_Ipv6Multicast_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.CreateFrom("IN IP6 FF15::101");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformationLine.IPAddress.Should().Be(IPAddress.Parse("FF15::101"));
            connectionInformationLine.MulticastTimeToLive.Should().Be(0);
            connectionInformationLine.NumberOfMulticastAddresses.Should().Be(3);
        }

        [TestMethod]
        public void CreateFrom_Ipv6MulticastMultipleAddresses_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.CreateFrom("IN IP6 FF15::101/3");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv6);
            connectionInformationLine.IPAddress.Should().Be(IPAddress.Parse("FF15::101"));
            connectionInformationLine.MulticastTimeToLive.Should().Be(0);
            connectionInformationLine.NumberOfMulticastAddresses.Should().Be(3);
        }

        [TestMethod]
        public void CreateFrom_Ipv4MulticastWithTtl_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.CreateFrom("IN IP4 224.2.36.42/127");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformationLine.IPAddress.Should().Be(IPAddress.Parse("224.2.36.42"));
            connectionInformationLine.MulticastTimeToLive.Should().Be(127);
            connectionInformationLine.NumberOfMulticastAddresses.Should().Be(0);
        }

        [TestMethod]
        public void CreateFrom_Ipv4MulticastWithTtlAndMultipleAddress_AllValuesAreCorrect()
        {
            var line = ConnectionInformationLine.CreateFrom("IN IP4 224.2.36.42/127/2");

            var connectionInformationLine = line.Result as ConnectionInformationLine;
            connectionInformationLine.NetType.Should().Be(NetType.Internet);
            connectionInformationLine.AddressType.Should().Be(AddressType.Ipv4);
            connectionInformationLine.IPAddress.Should().Be(IPAddress.Parse("224.2.36.42"));
            connectionInformationLine.MulticastTimeToLive.Should().Be(127);
            connectionInformationLine.NumberOfMulticastAddresses.Should().Be(2);
        }
    }
}
