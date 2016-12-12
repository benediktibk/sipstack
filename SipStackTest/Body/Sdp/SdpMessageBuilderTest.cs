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

        [TestMethod]
        public void AddSessionName_ValidInput_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddSessionName("asdfas asdg");

            var result = _messageBuilder.ToString();
            result.Should().Be("s=asdfas asdg\r\n");
        }

        [TestMethod]
        public void AddSessionDescription_ValidInput_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddSessionDescription("asdfas asdg");

            var result = _messageBuilder.ToString();
            result.Should().Be("i=asdfas asdg\r\n");
        }

        [TestMethod]
        public void AddSessionUri_ValidInput_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddSessionUri(new System.Uri("http://asdf.asdf.asdf/asdfg"));

            var result = _messageBuilder.ToString();
            result.Should().Be("u=http://asdf.asdf.asdf/asdfg\r\n");
        }

        [TestMethod]
        public void AddEmailAddress_ValidInputWithDisplayName_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddEmailAddress(new SipStack.Utils.EmailAddress("j.doe", "example.com", "Jane Doe"));

            var result = _messageBuilder.ToString();
            result.Should().Be("e=Jane Doe <j.doe@example.com>\r\n");
        }

        [TestMethod]
        public void AddEmailAddress_ValidInputWithoutDisplayName_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddEmailAddress(new SipStack.Utils.EmailAddress("j.doe", "example.com", string.Empty));

            var result = _messageBuilder.ToString();
            result.Should().Be("e=j.doe@example.com\r\n");
        }

        [TestMethod]
        public void AddPhoneNumber_ValidInputWithOnlyPhoneNumber_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddPhoneNumber(new SipStack.Utils.PhoneNumber("+16175556011", "", ""));

            var result = _messageBuilder.ToString();
            result.Should().Be("p=+16175556011\r\n");
        }

        [TestMethod]
        public void AddPhoneNumber_ValidInputWithNumberAndDomain_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddPhoneNumber(new SipStack.Utils.PhoneNumber("+16175556011", "global.gl", ""));

            var result = _messageBuilder.ToString();
            result.Should().Be("p=+16175556011@global.gl\r\n");
        }

        [TestMethod]
        public void AddPhoneNumber_ValidInputWithNumberAndDomainAndDisplayName_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddPhoneNumber(new SipStack.Utils.PhoneNumber("+1 617 555-6011", "global.gl", "Jane White"));

            var result = _messageBuilder.ToString();
            result.Should().Be("p=Jane White <+16175556011@global.gl>\r\n");
        }

        [TestMethod]
        public void AddPhoneNumber_ValidInputWithNumberAndDisplayName_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddPhoneNumber(new SipStack.Utils.PhoneNumber("+16175556011", "", "Jane White"));

            var result = _messageBuilder.ToString();
            result.Should().Be("p=Jane White <+16175556011>\r\n");
        }
    }
}
