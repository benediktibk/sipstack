using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using System.IO;
using FluentAssertions;

namespace SipStackTest
{
    [TestClass]
    public class MessageParserTest
    {
        [TestMethod]
        public void Parse_EmptyMessage_ParseError()
        {
            var messageParser = new MessageParser("");

            var parseResult = messageParser.Parse();

            parseResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Constructor_InvalidRequest_ParseError()
        {
            var messageParser = new MessageParser("BLUBBERREQUEST asdf SIP/2.0");

            var parseResult = messageParser.Parse();

            parseResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_ComplexInvite_SameInviteContentwise()
        {
            var messageParser = CreateParserFromFile("001_invite_in");

            var parseResult = messageParser.Parse();

            var result = parseResult.Message.ToString();
            var expectedResult = ReadFromFile("001_invite_out");
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void Parse_SimpleInvite_SameInviteContentwise()
        {
            var messageParser = CreateParserFromFile("002_invite_in");

            var parseResult = messageParser.Parse();

            var result = parseResult.Message.ToString();
            var expectedResult = ReadFromFile("002_invite_out");
            result.Should().Be(expectedResult);
        }

        private static MessageParser CreateParserFromFile(string file)
        {
            var fileContent = ReadFromFile(file);
            return new MessageParser(fileContent);
        }

        private static string ReadFromFile(string file)
        {
            return File.ReadAllText($"messages/{file}.txt");
        }
    }
}
