using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using SipStack.Body.Sdp;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class SdpMessageBuilderTest
    {
        private MessageBuilder _messageBuilder;
        private SdpMessageBuilder _sdpMessageBuilder;

        [TestInitialize]
        public void SetUp()
        {
            _messageBuilder = new MessageBuilder();
            _sdpMessageBuilder = new SdpMessageBuilder(_messageBuilder);
        }

        [TestMethod]
        public void AddProtocolVersion_ValidInput_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddProtocolVersion(6);

            var result = _messageBuilder.ToString();
            result.Should().Be("v=6\r\n");
        }

        [TestMethod]
        public void AddOriginator_ValidInput_CorrectLineAdded()
        {
            var originator = 
                new Originator(
                    "alice", 2890844526, 2890844526, SipStack.Network.NetType.Internet, 
                    SipStack.Network.AddressType.Ipv4,"1.2.4.5");

            _sdpMessageBuilder.AddOriginator(originator);

            var result = _messageBuilder.ToString();
            result.Should().Be("o=alice 2890844526 2890844526 IN IP4 1.2.4.5\r\n");
        }
    }
}
