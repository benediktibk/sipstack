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
        public void AddProtocolVersion_6_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddProtocolVersion(6);

            var result = _messageBuilder.ToString();
            result.Should().Be("v=6\r\n");
        }
    }
}
