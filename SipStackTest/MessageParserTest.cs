using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using System.IO;
using FluentAssertions;
using Moq;
using SipStack.Header;
using SipStack.Body;

namespace SipStackTest
{
    [TestClass]
    public class MessageParserTest
    {
        private MessageParser _messageParser;
        private MessageParser _messageParserWithMocks;
        private Mock<RequestLineParser> _requestLineParser;
        private Mock<HeaderFieldParser> _headerFieldParser;
        private Mock<HeaderParser> _headerParser;
        private Mock<BodyParserFactory> _bodyParserFactory;

        [TestInitialize]
        public void SetUp()
        {
            _messageParser = new MessageParser(new HeaderParser(new RequestLineParser(), new HeaderFieldParser()), new BodyParserFactory());
            _headerFieldParser = new Mock<HeaderFieldParser>();
            _requestLineParser = new Mock<RequestLineParser>();
            _bodyParserFactory = new Mock<BodyParserFactory>();
            _headerParser = new Mock<HeaderParser>();
            _messageParserWithMocks = new MessageParser(_headerParser.Object, _bodyParserFactory.Object);
        }

        [TestMethod]
        public void Parse_EmptyMessage_ParseError()
        {
            var parseResult = _messageParser.Parse("");

            parseResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Constructor_InvalidRequest_ParseError()
        {
            var parseResult = _messageParser.Parse("BLUBBERREQUEST asdf SIP/2.0");

            parseResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_ComplexInvite_SameInviteContentwise()
        {
            var message = ReadFromFile("001_invite_in");

            var parseResult = _messageParser.Parse(message);

            var result = parseResult.Result.ToString();
            var expectedResult = ReadFromFile("001_invite_out");
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void Parse_SimpleInvite_SameInviteContentwise()
        {
            var message = ReadFromFile("002_invite_in");

            var parseResult = _messageParser.Parse(message);

            var result = parseResult.Result.ToString();
            var expectedResult = ReadFromFile("002_invite_out");
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void Parse_SimpleInviteWithCarriageReturn_Success()
        {
            var message = ReadFromFile("003_invitewithcarriagereturn_in");

            var parseResult = _messageParser.Parse(message);

            parseResult.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_ComplexInviteWithoutBody_SameInviteContentwise()
        {
            var message = ReadFromFile("004_invite_in");

            var parseResult = _messageParser.Parse(message);

            var result = parseResult.Result.ToString();
            var expectedResult = ReadFromFile("004_invite_out");
            result.Should().Be(expectedResult);
        }

        private static string ReadFromFile(string file)
        {
            return File.ReadAllText($"messages/{file}.txt");
        }
    }
}
