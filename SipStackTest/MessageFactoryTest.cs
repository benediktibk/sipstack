using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using System.IO;
using FluentAssertions;

namespace SipStackTest
{
    [TestClass]
    public class MessageFactoryTest
    {
        [TestMethod]
        public void Parse_Invite_SameInviteContentwise()
        {
            var input = CreateInviteInput();

            var message = MessageFactory.Parse(input);

            var result = message.ToString();
            var expectedResult = CreateInviteResult();
            result.Should().Be(expectedResult);
        }

        private static string CreateInviteInput()
        {
            return File.ReadAllText(@"messages/invite_in.txt");
        }
        private static string CreateInviteResult()
        {
            return File.ReadAllText(@"messages/invite_out.txt");
        }
    }
}
