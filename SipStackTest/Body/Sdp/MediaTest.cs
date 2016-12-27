using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class MediaTest
    {
        [TestMethod]
        public void Parse_MediaTypeVideo_MediaTypeIsVideo()
        {
            var media = Media.Parse(@"video 49170/2 RTP/AVP 31");

            media.Result.MediaType.Should().Be(MediaType.Video);
        }

        [TestMethod]
        public void Parse_MediaTypeAudio_MediaTypeIsAudio()
        {
            var media = Media.Parse(@"audio 49170/2 RTP/AVP 31");
            
            media.Result.MediaType.Should().Be(MediaType.Audio);
        }

        [TestMethod]
        public void Parse_MediaTypeText_MediaTypeIsText()
        {
            var media = Media.Parse(@"text 49170/2 RTP/AVP 31");
            
            media.Result.MediaType.Should().Be(MediaType.Text);
        }

        [TestMethod]
        public void Parse_MediaTypeMessage_MediaTypeIsMessage()
        {
            var media = Media.Parse(@"message 49170/2 RTP/AVP 31");
            
            media.Result.MediaType.Should().Be(MediaType.Message);
        }

        [TestMethod]
        public void Parse_MediaTypeApplication_MediaTypeIsApplication()
        {
            var media = Media.Parse(@"application 49170/2 RTP/AVP 31");
            
            media.Result.MediaType.Should().Be(MediaType.Application);
        }

        [TestMethod]
        public void Parse_MediaTypeInvalid_Error()
        {
            var media = Media.Parse(@"asdf 49170/2 RTP/AVP 31");

            media.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_ContentInvalid_Error()
        {
            var media = Media.Parse(@"asdf asdf df");

            media.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Port49170_PortIs49170()
        {
            var media = Media.Parse(@"audio 49170/2 RTP/AVP 31");
            
            media.Result.Port.Should().Be(49170);
        }

        [TestMethod]
        public void Parse_PortCount2_PortCountIs2()
        {
            var media = Media.Parse(@"audio 49170/2 RTP/AVP 31");
            
            media.Result.PortCount.Should().Be(2);
        }

        [TestMethod]
        public void Parse_PortCountMissing_PortCountIs1()
        {
            var media = Media.Parse(@"audio 49170 RTP/AVP 31");
            
            media.Result.PortCount.Should().Be(1);
        }

        [TestMethod]
        public void Parse_RTPAVP_ProtocolIsRTPAVP()
        {
            var media = Media.Parse(@"audio 49170 RTP/AVP 31");
            
            media.Result.MediaTransportProtocol.Should().Be(MediaTransportProtocol.RtpAvp);
        }

        [TestMethod]
        public void Parse_RTPSAVP_ProtocolIsRTPSAVP()
        {
            var media = Media.Parse(@"audio 49170/2 RTP/SAVP 31");
            
            media.Result.MediaTransportProtocol.Should().Be(MediaTransportProtocol.RtpSavp);
        }

        [TestMethod]
        public void Parse_Udp_ProtocolIsUdp()
        {
            var media = Media.Parse(@"audio 49170/2 udp 31");
            
            media.Result.MediaTransportProtocol.Should().Be(MediaTransportProtocol.Udp);
        }

        [TestMethod]
        public void Parse_OnePayloadType_PayloadTypeIsCorrect()
        {
            var media = Media.Parse(@"audio 49170/2 udp 31");
            
            media.Result.MediaFormatDescriptions.Count.Should().Be(1);
            media.Result.MediaFormatDescriptions[0].Should().Be("31");
        }

        [TestMethod]
        public void Parse_ThreePayloadType_PayloadTypesAreCorrect()
        {
            var media = Media.Parse(@"audio 49170/2 udp 31 34 5");
            
            media.Result.MediaFormatDescriptions.Count.Should().Be(3);
            media.Result.MediaFormatDescriptions[0].Should().Be("31");
            media.Result.MediaFormatDescriptions[1].Should().Be("34");
            media.Result.MediaFormatDescriptions[2].Should().Be("5");
        }
    }
}
