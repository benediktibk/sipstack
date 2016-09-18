using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class EmailAddressLineTest
    {
        [TestMethod]
        public void Parse_ValidEmailAddress_AllValuesAreCorrect()
        {
            var line = EmailAddressLine.Parse(@"j.doe@example.com (Jane Doe)");

            var emailAddressLine = line.Result as EmailAddressLine;
            emailAddressLine.EmailAddress.LocalPart.Should().Be("j.doe");
            emailAddressLine.EmailAddress.Domain.Should().Be("example.com");
            emailAddressLine.EmailAddress.DisplayName.Should().Be("Jane Doe");
        }
    }
}
