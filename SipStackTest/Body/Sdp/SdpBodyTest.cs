using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using SipStack;
using SipStack.Network;
using SipStack.Utils;
using System.Collections.Generic;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class SdpBodyTest
    {
        private MessageBuilder _messageBuilder;

        [TestInitialize]
        public void SetUp()
        {
            _messageBuilder = new MessageBuilder();
        }

        [TestMethod]
        public void AddTo_BandwidthTypeUnknown_UnknownBandwidthIsNotInMessage()
        {
            var body = new SdpBody(
                0, 
                new Originator("jane.doe", 23, 35, NetType.Internet, AddressType.Ipv4, "originator.host"),
                "session title", 
                "session description", 
                new Uri("http://uri.org"), 
                new EmailAddress("email", "domain.org", "Jane Doe"),
                new PhoneNumber("+13546546", "sub.domain.org", "Jane Doe 2"), 
                new List<ConnectionInformation>
                {
                    new ConnectionInformation(NetType.Internet, AddressType.Ipv4, "master.host.org")
                },
                new List<Bandwidth>
                {
                    new Bandwidth(BandwidthType.Unknown, 123),
                    new Bandwidth(BandwidthType.ApplicationSpecific, 234)
                }, 
                new List<TimeDescription>(),
                new List<TimeZoneAdjustment>(), 
                new EncryptionKey(EncryptionKeyType.Clear, "THISISTHEKEYTOTHEUNIVERSE"), 
                new List<SipStack.Body.Sdp.Attribute>(),
                new List<MediaDescription>
                {
                    new MediaDescription(
                        new Media(MediaType.Audio, 12345, 5, MediaTransportProtocol.Udp, new List<string> { "8" }), 
                        "media title",
                        new List<ConnectionInformation>(), 
                        new List<Bandwidth>(), 
                        null, 
                        new List<SipStack.Body.Sdp.Attribute>())
                });

            body.AddTo(_messageBuilder);

            var result = _messageBuilder.ToString();
            result.Should().Be("v=0\r\no=jane.doe 23 35 IN IP4 originator.host\r\ns=session title\r\ni=session description\r\nu=http://uri.org/\r\ne=Jane Doe <email@domain.org>\r\np=Jane Doe 2 <+13546546@sub.domain.org>\r\nc=IN IP4 master.host.org\r\nb=AS:234\r\nk=clear:THISISTHEKEYTOTHEUNIVERSE\r\nm=audio 12345/5 udp 8\r\ni=media title");
        }
    }
}
