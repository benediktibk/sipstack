using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class LineParserTest
    {
        private LineParser _lineParser;

        [TestInitialize]
        public void SetUp()
        {
            _lineParser = new LineParser();
        }

        [TestMethod]
        public void Parse_VersionLine_VersionLine()
        {
            var parseResult = _lineParser.Parse(@"v=0");

            parseResult.Result.Should().BeOfType(typeof(VersionLine));
        }

        [TestMethod]
        public void Parse_OriginatorLine_OriginatorLine()
        {
            var parseResult = _lineParser.Parse(@"o=jdoe 2890844526 2890842807 IN IP4 10.47.16.5");

            parseResult.Result.Should().BeOfType(typeof(OriginatorLine));
        }

        [TestMethod]
        public void Parse_SessionNameLine_SessionNameLine()
        {
            var parseResult = _lineParser.Parse(@"s=SDP Seminar");

            parseResult.Result.Should().BeOfType(typeof(SessionNameLine));
        }

        [TestMethod]
        public void Parse_DescriptionLine_DescriptionLine()
        {
            var parseResult = _lineParser.Parse(@"i=A Seminar on the session description protocol");

            parseResult.Result.Should().BeOfType(typeof(DescriptionLine));
        }

        [TestMethod]
        public void Parse_UriLine_UriLine()
        {
            var parseResult = _lineParser.Parse(@"u=http://www.example.com/seminars/sdp.pdf");

            parseResult.Result.Should().BeOfType(typeof(SipStack.Body.Sdp.UriLine));
        }

        [TestMethod]
        public void Parse_EmailAddressLine_EmailAddressLine()
        {
            var parseResult = _lineParser.Parse(@"e=j.doe@example.com (Jane Doe)");

            parseResult.Result.Should().BeOfType(typeof(SipStack.Body.Sdp.EmailAddressLine));
        }

        [TestMethod]
        public void Parse_ConnectionInformationLine_ConnectionInformationLine()
        {
            var parseResult = _lineParser.Parse(@"c=IN IP4 224.2.17.12/127");

            parseResult.Result.Should().BeOfType(typeof(ConnectionInformationLine));
        }

        [TestMethod]
        public void Parse_TimeLine_TimeLine()
        {
            var parseResult = _lineParser.Parse(@"t=2873397496 2873404696");

            parseResult.Result.Should().BeOfType(typeof(TimeLine));
        }

        [TestMethod]
        public void Parse_AttributeLine_AttributeLine()
        {
            var parseResult = _lineParser.Parse(@"a=recvonly");

            parseResult.Result.Should().BeOfType(typeof(AttributeLine));
        }

        [TestMethod]
        public void Parse_MediaLine_MediaLine()
        {
            var parseResult = _lineParser.Parse(@"m=audio 49170 RTP/AVP 0");

            parseResult.Result.Should().BeOfType(typeof(MediaLine));
        }

        [TestMethod]
        public void Parse_PhoneNumberLine_PhoneNumberLine()
        {
            var parseResult = _lineParser.Parse(@"p=+1 617 555-6011");

            parseResult.Result.Should().BeOfType(typeof(PhoneNumberLine));
        }

        [TestMethod]
        public void Parse_BandwithLine_BandwithLine()
        {
            var parseResult = _lineParser.Parse(@"b=CT:128");

            parseResult.Result.Should().BeOfType(typeof(BandwidthLine));
        }

        [TestMethod]
        public void Parse_RepeatLine_RepeatLine()
        {
            var parseResult = _lineParser.Parse(@"r=604800 3600 0 90000");

            parseResult.Result.Should().BeOfType(typeof(RepeatLine));
        }

        [TestMethod]
        public void Parse_TimeZoneLine_TimeZoneLine()
        {
            var parseResult = _lineParser.Parse(@"z=2882844526 -1h 2898848070 0");

            parseResult.Result.Should().BeOfType(typeof(TimeZoneLine));
        }

        [TestMethod]
        public void Parse_EncryptionKeyLine_EncryptionKeyLine()
        {
            var parseResult = _lineParser.Parse(@"k=prompt");

            parseResult.Result.Should().BeOfType(typeof(EncryptionKeyLine));
        }
    }
}
