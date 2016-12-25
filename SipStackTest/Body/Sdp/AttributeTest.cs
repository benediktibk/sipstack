using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class AttributeTest
    {
        [TestMethod]
        public void Parse_recvonly_NameAndValueAreCorrect()
        {
            var line = Attribute.Parse(@"recvonly");

            var attributeLine = line.Result as Attribute;
            attributeLine.Name.Should().Be("recvonly");
            attributeLine.Value.Should().Be("");
        }

        [TestMethod]
        public void Parse_rtpmap_NameAndValueAreCorrect()
        {
            var line = Attribute.Parse(@"rtpmap:8 PCMA/8000/1");

            var attributeLine = line.Result as Attribute;
            attributeLine.Name.Should().Be("rtpmap");
            attributeLine.Value.Should().Be("8 PCMA/8000/1");
        }
    }
}
