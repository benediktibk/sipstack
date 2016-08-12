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
        public void Parse_ComplexInvite_SameInviteContentwise()
        {
            var messageParser = CreateParserFromFile("001_invite_in");

            var message = messageParser.Parse();

            var result = message.ToString();
            var expectedResult = ReadFromFile("001_invite_out");
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void Parse_SimpleInvite_SameInviteContentwise()
        {
            var messageParser = CreateParserFromFile("002_invite_in");

            var message = messageParser.Parse();

            var result = message.ToString();
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
