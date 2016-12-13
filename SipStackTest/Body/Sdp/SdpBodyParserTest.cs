using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using System.Collections.Generic;
using FluentAssertions;
using System.IO;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class SdpBodyParserTest
    {
        private LineParser _lineParser;
        private SdpBodyParser _sdpBodyParser;
        private List<string> _onlyMandatoryLines;
        private List<string> _allOptionalLines;
        private List<string> _multipleConnectionData;

        [TestInitialize]
        public void SetUp()
        {
            _lineParser = new LineParser();
            _sdpBodyParser = new SdpBodyParser(_lineParser);

            _onlyMandatoryLines = new List<string>
            {
                "v=0",
                "o=asdf 132456 132456 IN IP4 1.5.8.9",
                "s=JUHU"
            };

            _allOptionalLines = new List<string>
            {
                "v=0",
                "o=asdf 132456 132456 IN IP4 1.5.8.9",
                "s=JUHU",
                "i=eine beschreibung",
                "u=http://asdf.asdf",
                "e=huber.depp@affenhausen.at",
                "p=+494254654312",
                "c=IN IP4 224.2.36.42/127",
                "b=CT:1234",
                "b=AS:58",
                "t=3034423619 3042462419",
                "r=604800 100 0 90000",
                "r=604800 200 0 90300",
                "t=1654 4987897",
                "z=2882844526 -1h 2898848070 0",
                "k=clear:ASDF6465asdf456",
                "a=recvonly",
                "a=orient:landscape",
                "m=audio 41736 RTP/AVP 8 97 98 99 18 96 100",
                "i=MEDIAAAAAAA",
                "c=IN IP4 10.122.69.145",
                "b=AS:90",
                "k=prompt",
                "a=rtpmap:8 PCMA/8000/1",
                "a=rtpmap:97 AMR-WB/16000/1",
                "m=video 15648 RTP/AVP 34"
            };

            _multipleConnectionData = new List<string>
            {
                "v=0",
                "o=asdf 132456 132456 IN IP4 1.5.8.9",
                "s=JUHU",
                "i=eine beschreibung",
                "u=http://asdf.asdf",
                "e=huber.depp@affenhausen.at",
                "p=+494254654312",
                "c=IN IP4 172.26.8.45",
                "c=IN IP4 172.26.8.46",
                "b=CT:1234",
                "b=AS:58",
                "t=3034423619 3042462419",
                "r=604800 100 0 90000",
                "r=604800 200 0 90300",
                "t=1654 4987897",
                "z=2882844526 -1h 2898848070 0",
                "k=clear:ASDF6465asdf456",
                "a=recvonly",
                "a=orient:landscape",
                "m=audio 41736 RTP/AVP 8 97 98 99 18 96 100",
                "i=MEDIAAAAAAA",
                "c=IN IP4 10.122.69.145",
                "c=IN IP6 0015::101",
                "b=AS:90",
                "k=prompt",
                "a=rtpmap:8 PCMA/8000/1",
                "a=rtpmap:97 AMR-WB/16000/1",
                "m=video 15648 RTP/AVP 34"
            };
        }

        [TestMethod]
        public void Parse_OnlyMandatoryLines_VersionIsCorrect()
        {
            var result = _sdpBodyParser.Parse(_onlyMandatoryLines, 0, 2);

            var sdpBody = result.Result as SdpBody;
            sdpBody.ProtocolVersion.Should().Be(0);
        }

        [TestMethod]
        public void Parse_OriginatorMissing_ParseError()
        {
            var result = _sdpBodyParser.Parse(new List<string> { "v=0", "s=JUHU" }, 0, 1);

            result.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_AllOptionalLines_ParseSuccess()
        {
            var result = _sdpBodyParser.Parse(_allOptionalLines, 0, _allOptionalLines.Count - 1);

            result.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_AllOptionalLinesWithWrongLineAtEnd_ParseError()
        {
            _allOptionalLines.Add("o=asdf 132456 132456 IN IP4 1.5.8.9");
            var result = _sdpBodyParser.Parse(_allOptionalLines, 0, _allOptionalLines.Count - 1);

            result.IsError.Should().BeTrue();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_NotEnoughLines_ThrowsArgumentException()
        {
            var result = _sdpBodyParser.Parse(_allOptionalLines, 0, _allOptionalLines.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_NegativeStart_ThrowsArgumentException()
        {
            var result = _sdpBodyParser.Parse(_allOptionalLines, -1, _allOptionalLines.Count - 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_EndSmallerThanStart_ThrowsArgumentException()
        {
            var result = _sdpBodyParser.Parse(_allOptionalLines, 5, 2);
        }

        [TestMethod]
        public void Parse_ValidBody_SdpBody()
        {
            var lines = ReadFromFile("005_sdp");
            var result = _sdpBodyParser.Parse(lines, 4, 27);

            result.Result.Should().BeOfType(typeof(SdpBody));
        }

        [TestMethod]
        public void Parse_MultipleConnectionData_Success()
        {
            var result = _sdpBodyParser.Parse(_multipleConnectionData, 0, _multipleConnectionData.Count - 1);

            result.IsSuccess.Should().BeTrue();
        }

        private static IList<string> ReadFromFile(string file)
        {
            var content = File.ReadAllText($"messages/{file}.txt");
            return content.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        }
    }
}
