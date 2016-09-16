using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;
using SipStack.Network;
using System.Net;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class AttributeLineTest
    {
        [TestMethod]
        public void CreateFrom_recvonly_NameAndValueAreCorrect()
        {
            var line = AttributeLine.CreateFrom(@"recvonly");

            var attributeLine = line.Result as AttributeLine;
            attributeLine.Name.Should().Be("recvonly");
            attributeLine.Value.Should().Be("");
        }

        [TestMethod]
        public void CreateFrom_rtpmap_NameAndValueAreCorrect()
        {
            var line = AttributeLine.CreateFrom(@"rtpmap:8 PCMA/8000/1");

            var attributeLine = line.Result as AttributeLine;
            attributeLine.Name.Should().Be("rtpmap");
            attributeLine.Value.Should().Be("8 PCMA/8000/1");
        }
    }
}
