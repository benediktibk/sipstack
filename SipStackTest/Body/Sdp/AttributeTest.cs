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
            var attribute = Attribute.Parse(@"recvonly");

            attribute.Result.Name.Should().Be("recvonly");
            attribute.Result.Value.Should().Be("");
        }

        [TestMethod]
        public void Parse_rtpmap_NameAndValueAreCorrect()
        {
            var attribute = Attribute.Parse(@"rtpmap:8 PCMA/8000/1");
            
            attribute.Result.Name.Should().Be("rtpmap");
            attribute.Result.Value.Should().Be("8 PCMA/8000/1");
        }
    }
}
