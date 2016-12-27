using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;
using SipStack.Network;
using System.Net;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class OriginatorTest
    {
        [TestMethod]
        public void Parse_ValidExample_AllValuesAreCorrect()
        {
            var originator = Originator.Parse(@"jdoe 2890844526 2890842807 IN IP4 10.47.16.5");
            
            originator.Result.Username.Should().Be("jdoe");
            originator.Result.SessionId.Should().Be(2890844526);
            originator.Result.SessionVersion.Should().Be(2890842807);
            originator.Result.NetType.Should().Be(NetType.Internet);
            originator.Result.AddressType.Should().Be(AddressType.Ipv4);
            originator.Result.Host.Should().Be("10.47.16.5");
        }

        [TestMethod]
        public void Parse_ValidExampleWithIpv6_IpIsCorrect()
        {
            var originator = Originator.Parse(@"jdoe 2890844526 2890842807 IN IP6 2001:0db8:0000:0000:0000:ff00:0042:8329");
            
            originator.Result.AddressType.Should().Be(AddressType.Ipv6);
            originator.Result.Host.Should().Be("2001:0db8:0000:0000:0000:ff00:0042:8329");
        }

        [TestMethod]
        public void Parse_ValidExampleWithHostName_HostNameIsCorrect()
        {
            var originator = Originator.Parse(@"jdoe 2890844526 2890842807 IN IP6 asdf.blub.at");
            
            originator.Result.AddressType.Should().Be(AddressType.Ipv6);
            originator.Result.Host.Should().Be("asdf.blub.at");
        }
    }
}
