using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class MediaLineTest
    {
        [TestMethod]
        public void Parse_MediaTypeVideo_MediaTypeIsVideo()
        {
            var line = MediaLine.Parse(@"video 49170/2 RTP/AVP 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.MediaType.Should().Be(MediaType.Video);
        }

        [TestMethod]
        public void Parse_MediaTypeAudio_MediaTypeIsAudio()
        {
            var line = MediaLine.Parse(@"audio 49170/2 RTP/AVP 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.MediaType.Should().Be(MediaType.Audio);
        }

        [TestMethod]
        public void Parse_MediaTypeText_MediaTypeIsText()
        {
            var line = MediaLine.Parse(@"text 49170/2 RTP/AVP 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.MediaType.Should().Be(MediaType.Text);
        }

        [TestMethod]
        public void Parse_MediaTypeMessage_MediaTypeIsMessage()
        {
            var line = MediaLine.Parse(@"message 49170/2 RTP/AVP 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.MediaType.Should().Be(MediaType.Message);
        }

        [TestMethod]
        public void Parse_MediaTypeApplication_MediaTypeIsApplication()
        {
            var line = MediaLine.Parse(@"application 49170/2 RTP/AVP 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.MediaType.Should().Be(MediaType.Application);
        }

        [TestMethod]
        public void Parse_MediaTypeInvalid_Error()
        {
            var line = MediaLine.Parse(@"asdf 49170/2 RTP/AVP 31");

            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_ContentInvalid_Error()
        {
            var line = MediaLine.Parse(@"asdf asdf df");

            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Port49170_PortIs49170()
        {
            var line = MediaLine.Parse(@"audio 49170/2 RTP/AVP 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.Port.Should().Be(49170);
        }

        [TestMethod]
        public void Parse_PortCount2_PortCountIs2()
        {
            var line = MediaLine.Parse(@"audio 49170/2 RTP/AVP 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.PortCount.Should().Be(2);
        }

        [TestMethod]
        public void Parse_PortCountMissing_PortCountIs1()
        {
            var line = MediaLine.Parse(@"audio 49170 RTP/AVP 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.PortCount.Should().Be(1);
        }

        [TestMethod]
        public void Parse_RTPAVP_ProtocolIsRTPAVP()
        {
            var line = MediaLine.Parse(@"audio 49170 RTP/AVP 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.MediaTransportProtocol.Should().Be(MediaTransportProtocol.RtpAvp);
        }

        [TestMethod]
        public void Parse_RTPSAVP_ProtocolIsRTPSAVP()
        {
            var line = MediaLine.Parse(@"audio 49170/2 RTP/SAVP 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.MediaTransportProtocol.Should().Be(MediaTransportProtocol.RtpSavp);
        }

        [TestMethod]
        public void Parse_Udp_ProtocolIsUdp()
        {
            var line = MediaLine.Parse(@"audio 49170/2 udp 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.MediaTransportProtocol.Should().Be(MediaTransportProtocol.Udp);
        }

        [TestMethod]
        public void Parse_OnePayloadType_PayloadTypeIsCorrect()
        {
            var line = MediaLine.Parse(@"audio 49170/2 udp 31");

            var mediaLine = line.Result as MediaLine;
            mediaLine.MediaFormatDescriptions.Count.Should().Be(1);
            mediaLine.MediaFormatDescriptions[0].Should().Be("31");
        }

        [TestMethod]
        public void Parse_ThreePayloadType_PayloadTypesAreCorrect()
        {
            var line = MediaLine.Parse(@"audio 49170/2 udp 31 34 5");

            var mediaLine = line.Result as MediaLine;
            mediaLine.MediaFormatDescriptions.Count.Should().Be(3);
            mediaLine.MediaFormatDescriptions[0].Should().Be("31");
            mediaLine.MediaFormatDescriptions[1].Should().Be("34");
            mediaLine.MediaFormatDescriptions[2].Should().Be("5");
        }
    }
}
