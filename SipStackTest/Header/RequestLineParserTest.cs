using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using FluentAssertions;
using SipStack.Header;

namespace SipStackTest.Header
{
    [TestClass]
    public class RequestLineParserTest
    {
        private RequestLineParser _parser;

        [TestInitialize]
        public void SetUp()
        {
            _parser = new RequestLineParser();
        }

        [TestMethod]
        public void Parse_EmptyLine_Error()
        {
            var result = _parser.Parse("");

            result.Error.Should().Be(ParseError.InvalidRequestLine);
        }

        [TestMethod]
        public void Parse_WrongSipVersion_InvalidSipVersion()
        {
            var result = _parser.Parse("INVITE 1234 SIP/3.0");

            result.Error.Should().Be(ParseError.InvalidSipVersion);
        }

        [TestMethod]
        public void Parse_RequestBlub_InvalidRequest()
        {
            var result = _parser.Parse("BLUB 1234 SIP/2.0");

            result.Error.Should().Be(ParseError.InvalidRequestMethod);
        }
    }
}
