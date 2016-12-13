using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using SipStack.Body.Sdp;
using System;
using System.Collections.Generic;

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
            result.Should().Be("v=6");
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
            result.Should().Be("o=alice 2890844526 2890844526 IN IP4 1.2.4.5");
        }

        [TestMethod]
        public void AddSessionName_ValidInput_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddSessionName("asdfas asdg");

            var result = _messageBuilder.ToString();
            result.Should().Be("s=asdfas asdg");
        }

        [TestMethod]
        public void AddSessionDescription_ValidInput_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddSessionDescription("asdfas asdg");

            var result = _messageBuilder.ToString();
            result.Should().Be("i=asdfas asdg");
        }

        [TestMethod]
        public void AddSessionUri_ValidInput_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddSessionUri(new System.Uri("http://asdf.asdf.asdf/asdfg"));

            var result = _messageBuilder.ToString();
            result.Should().Be("u=http://asdf.asdf.asdf/asdfg");
        }

        [TestMethod]
        public void AddEmailAddress_ValidInputWithDisplayName_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddEmailAddress(new SipStack.Utils.EmailAddress("j.doe", "example.com", "Jane Doe"));

            var result = _messageBuilder.ToString();
            result.Should().Be("e=Jane Doe <j.doe@example.com>");
        }

        [TestMethod]
        public void AddEmailAddress_ValidInputWithoutDisplayName_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddEmailAddress(new SipStack.Utils.EmailAddress("j.doe", "example.com", string.Empty));

            var result = _messageBuilder.ToString();
            result.Should().Be("e=j.doe@example.com");
        }

        [TestMethod]
        public void AddPhoneNumber_ValidInputWithOnlyPhoneNumber_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddPhoneNumber(new SipStack.Utils.PhoneNumber("+16175556011", "", ""));

            var result = _messageBuilder.ToString();
            result.Should().Be("p=+16175556011");
        }

        [TestMethod]
        public void AddPhoneNumber_ValidInputWithNumberAndDomain_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddPhoneNumber(new SipStack.Utils.PhoneNumber("+16175556011", "global.gl", ""));

            var result = _messageBuilder.ToString();
            result.Should().Be("p=+16175556011@global.gl");
        }

        [TestMethod]
        public void AddPhoneNumber_ValidInputWithNumberAndDomainAndDisplayName_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddPhoneNumber(new SipStack.Utils.PhoneNumber("+1 617 555-6011", "global.gl", "Jane White"));

            var result = _messageBuilder.ToString();
            result.Should().Be("p=Jane White <+16175556011@global.gl>");
        }

        [TestMethod]
        public void AddPhoneNumber_ValidInputWithNumberAndDisplayName_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddPhoneNumber(new SipStack.Utils.PhoneNumber("+16175556011", "", "Jane White"));

            var result = _messageBuilder.ToString();
            result.Should().Be("p=Jane White <+16175556011>");
        }

        [TestMethod]
        public void AddConnectionInformation_ValidInputWithUnicast_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddConnectionInformation(
                new ConnectionInformation(SipStack.Network.NetType.Internet, SipStack.Network.AddressType.Ipv4, "15.6.4.9"));

            var result = _messageBuilder.ToString();
            result.Should().Be("c=IN IP4 15.6.4.9");
        }

        [TestMethod]
        public void AddConnectionInformation_ValidInputWithIpv4MulticastAndMultipleAddresses_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddConnectionInformation(
                new ConnectionInformation(SipStack.Network.NetType.Internet, SipStack.Network.AddressType.Ipv4, "15.6.4.9", 6, 123));

            var result = _messageBuilder.ToString();
            result.Should().Be("c=IN IP4 15.6.4.9/123/6");
        }

        [TestMethod]
        public void AddConnectionInformation_ValidInputWithIpv4Multicast_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddConnectionInformation(
                new ConnectionInformation(SipStack.Network.NetType.Internet, SipStack.Network.AddressType.Ipv4, "15.6.4.9", 1, 123));

            var result = _messageBuilder.ToString();
            result.Should().Be("c=IN IP4 15.6.4.9/123");
        }

        [TestMethod]
        public void AddConnectionInformation_ValidInputWithIpv6Multicast_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddConnectionInformation(
                new ConnectionInformation(SipStack.Network.NetType.Internet, SipStack.Network.AddressType.Ipv6, "FF15::101"));

            var result = _messageBuilder.ToString();
            result.Should().Be("c=IN IP6 FF15::101");
        }

        [TestMethod]
        public void AddConnectionInformation_ValidInputWithIpv6MulticastAndMultipleAddresses_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddConnectionInformation(
                new ConnectionInformation(SipStack.Network.NetType.Internet, SipStack.Network.AddressType.Ipv6, "FF15::101", 6));

            var result = _messageBuilder.ToString();
            result.Should().Be("c=IN IP6 FF15::101/6");
        }

        [TestMethod]
        public void AddBandwidth_ValidInput_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddBandwidth(new Bandwidth(BandwidthType.ApplicationSpecific, 123));

            var result = _messageBuilder.ToString();
            result.Should().Be("b=AS:123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddBandwidth_TypeUnkown_Exception()
        {
            _sdpMessageBuilder.AddBandwidth(new Bandwidth(BandwidthType.Unknown, 123));
        }

        [TestMethod]
        public void AddTiming_ValidInput_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddTiming(new Timing(new DateTime(1998, 12, 13, 14, 50, 0), new DateTime(1998, 12, 13, 15, 00, 0)));

            var result = _messageBuilder.ToString();
            result.Should().Be("t=3122549400 3122550000");
        }

        [TestMethod]
        public void AddTiming_ValidInputUnbounded_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddTiming(new Timing(new DateTime(1998, 12, 13, 14, 50, 0), DateTime.MaxValue));

            var result = _messageBuilder.ToString();
            result.Should().Be("t=3122549400 0");
        }

        [TestMethod]
        public void AddRepeat_ValidInput_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddRepeat(new Repeat(new TimeSpan(7*24, 0, 0), new TimeSpan(1, 30, 0), new TimeSpan(0, 0, 45), new TimeSpan(0, 0, 90)));

            var result = _messageBuilder.ToString();
            result.Should().Be("r=604800 5400 45 90");
        }

        [TestMethod]
        public void AddTimeZoneAdjustment_NoAdjustments_CorrectLineAdded()
        {
            var adjustments = new List<TimeZoneAdjustment>();

            _sdpMessageBuilder.AddTimeZoneAdjustment(adjustments);

            var result = _messageBuilder.ToString();
            result.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void AddTimeZoneAdjustment_OneAdjustment_CorrectLineAdded()
        {
            var adjustments = new List<TimeZoneAdjustment>()
            {
                new TimeZoneAdjustment(123123, 123)
            };

            _sdpMessageBuilder.AddTimeZoneAdjustment(adjustments);

            var result = _messageBuilder.ToString();
            result.Should().Be("z=123123 123");
        }

        [TestMethod]
        public void AddTimeZoneAdjustment_ThreeAdjustments_CorrectLineAdded()
        {
            var adjustments = new List<TimeZoneAdjustment>()
            {
                new TimeZoneAdjustment(123123, 123),
                new TimeZoneAdjustment(45645, 987),
                new TimeZoneAdjustment(16987913, 16546)
            };

            _sdpMessageBuilder.AddTimeZoneAdjustment(adjustments);

            var result = _messageBuilder.ToString();
            result.Should().Be("z=123123 123 45645 987 16987913 16546");
        }

        [TestMethod]
        public void AddEncryptionKey_Prompt_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddEncryptionKey(new EncryptionKey(EncryptionKeyType.Prompt, ""));

            var result = _messageBuilder.ToString();
            result.Should().Be("k=prompt");
        }

        [TestMethod]
        public void AddEncryptionKey_Clear_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddEncryptionKey(new EncryptionKey(EncryptionKeyType.Clear, "asdf465das4"));

            var result = _messageBuilder.ToString();
            result.Should().Be("k=clear:asdf465das4");
        }

        [TestMethod]
        public void AddEncryptionKey_Base64_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddEncryptionKey(new EncryptionKey(EncryptionKeyType.Base64, "YXNkZjQ2NWRhczQ="));

            var result = _messageBuilder.ToString();
            result.Should().Be("k=base64:YXNkZjQ2NWRhczQ=");
        }

        [TestMethod]
        public void AddEncryptionKey_Uri_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddEncryptionKey(new EncryptionKey(EncryptionKeyType.Uri, "http://this.is.the.magic.domain.org/keyForMe"));

            var result = _messageBuilder.ToString();
            result.Should().Be("k=uri:http://this.is.the.magic.domain.org/keyForMe");
        }

        [TestMethod]
        public void AddAttribute_Flag_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddAttribute(new SipStack.Body.Sdp.Attribute("recvonly"));

            var result = _messageBuilder.ToString();
            result.Should().Be("a=recvonly");
        }

        [TestMethod]
        public void AddAttribute_Value_CorrectLineAdded()
        {
            _sdpMessageBuilder.AddAttribute(new SipStack.Body.Sdp.Attribute("orient", "landscape"));

            var result = _messageBuilder.ToString();
            result.Should().Be("a=orient:landscape");
        }

        [TestMethod]
        public void AddMedia_OnePortAndOneType_CorrectLineAdded()
        {
            var media = new Media(
                MediaType.Audio, 45897, 1, MediaTransportProtocol.Udp,
                new List<string> { "8" });

            _sdpMessageBuilder.AddMedia(media);

            var result = _messageBuilder.ToString();
            result.Should().Be("m=audio 45897 udp 8");
        }

        [TestMethod]
        public void AddMedia_ThreePortsAndOneType_CorrectLineAdded()
        {
            var media = new Media(
                MediaType.Video, 45897, 3, MediaTransportProtocol.RtpAvp,
                new List<string> { "8" });

            _sdpMessageBuilder.AddMedia(media);

            var result = _messageBuilder.ToString();
            result.Should().Be("m=video 45897/3 RTP/AVP 8");
        }

        [TestMethod]
        public void AddMedia_ThreePortsAndMultipleType_CorrectLineAdded()
        {
            var media = new Media(
                MediaType.Video, 45897, 3, MediaTransportProtocol.RtpAvp,
                new List<string> { "8", "45", "asdf" });

            _sdpMessageBuilder.AddMedia(media);

            var result = _messageBuilder.ToString();
            result.Should().Be("m=video 45897/3 RTP/AVP 8 45 asdf");
        }
    }
}
