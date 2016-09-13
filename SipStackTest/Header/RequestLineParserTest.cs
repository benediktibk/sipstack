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
        public void Parse_EmptyLine_ParseError()
        {
            var result = _parser.Parse("");

            result.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_WrongSipVersion_ParseError()
        {
            var result = _parser.Parse("INVITE 1234 SIP/3.0");

            result.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_RequestBlub_ParseError()
        {
            var result = _parser.Parse("BLUB 1234 SIP/2.0");

            result.IsError.Should().BeTrue();
        }
    }
}
