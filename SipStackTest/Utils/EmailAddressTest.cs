using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Utils;

namespace SipStackTest.Utils
{
    [TestClass]
    public class EmailAddressTest
    {
        [TestMethod]
        public void Parse_NoDisplayName_AllValuesAreCorrect()
        {
            var parseResult = EmailAddress.Parse("j.doe@example.com");

            var emailAddress = parseResult.Result;
            emailAddress.DisplayName.Should().Be("");
            emailAddress.LocalPart.Should().Be("j.doe");
            emailAddress.Domain.Should().Be("example.com");
        }

        [TestMethod]
        public void Parse_NoDisplayNameWithAtInLocalPart_AllValuesAreCorrect()
        {
            var parseResult = EmailAddress.Parse("j@doe@example.com");

            var emailAddress = parseResult.Result;
            emailAddress.DisplayName.Should().Be("");
            emailAddress.LocalPart.Should().Be("j@doe");
            emailAddress.Domain.Should().Be("example.com");
        }

        [TestMethod]
        public void Parse_DisplayNameAfterAddress_AllValuesAreCorrect()
        {
            var parseResult = EmailAddress.Parse("j.doe@example.com (John Doe)");

            var emailAddress = parseResult.Result;
            emailAddress.DisplayName.Should().Be("John Doe");
            emailAddress.LocalPart.Should().Be("j.doe");
            emailAddress.Domain.Should().Be("example.com");
        }

        [TestMethod]
        public void Parse_DisplayNameAfterAddressWithAtInLocalPart_AllValuesAreCorrect()
        {
            var parseResult = EmailAddress.Parse("j.doe@blub@example.com (John Doe)");

            var emailAddress = parseResult.Result;
            emailAddress.DisplayName.Should().Be("John Doe");
            emailAddress.LocalPart.Should().Be("j.doe@blub");
            emailAddress.Domain.Should().Be("example.com");
        }

        [TestMethod]
        public void Parse_DisplayNameBeforeAddress_AllValuesAreCorrect()
        {
            var parseResult = EmailAddress.Parse("John Doe <j.doe@example.com>");

            var emailAddress = parseResult.Result;
            emailAddress.DisplayName.Should().Be("John Doe");
            emailAddress.LocalPart.Should().Be("j.doe");
            emailAddress.Domain.Should().Be("example.com");
        }

        [TestMethod]
        public void Parse_DisplayNameBeforeAddressWithAtInLocalPart_AllValuesAreCorrect()
        {
            var parseResult = EmailAddress.Parse("John Doe <j.doe@blub@example.com>");

            var emailAddress = parseResult.Result;
            emailAddress.DisplayName.Should().Be("John Doe");
            emailAddress.LocalPart.Should().Be("j.doe@blub");
            emailAddress.Domain.Should().Be("example.com");
        }

        [TestMethod]
        public void Parse_MissingBracket_Error()
        {
            var parseResult = EmailAddress.Parse("John Doe <j.doe@blub@example.com");

            parseResult.IsError.Should().BeTrue();
        }
    }
}
