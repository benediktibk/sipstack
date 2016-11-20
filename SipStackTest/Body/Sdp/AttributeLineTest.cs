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
        public void Parse_recvonly_NameAndValueAreCorrect()
        {
            var line = AttributeLine.Parse(@"recvonly");

            var attributeLine = line.Result as AttributeLine;
            attributeLine.Attribute.Name.Should().Be("recvonly");
            attributeLine.Attribute.Value.Should().Be("");
        }

        [TestMethod]
        public void Parse_rtpmap_NameAndValueAreCorrect()
        {
            var line = AttributeLine.Parse(@"rtpmap:8 PCMA/8000/1");

            var attributeLine = line.Result as AttributeLine;
            attributeLine.Attribute.Name.Should().Be("rtpmap");
            attributeLine.Attribute.Value.Should().Be("8 PCMA/8000/1");
        }
    }
}
