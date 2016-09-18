using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class PhoneNumberLineTest
    {
        [TestMethod]
        public void CreateFrom_ValidPhoneNumber_AllValuesAreCorrect()
        {
            var line = PhoneNumberLine.CreateFrom("Jane White <+1235325@asdf.blub.at>");

            var phoneNumberLine = line.Result as PhoneNumberLine;
            phoneNumberLine.PhoneNumber.DisplayName.Should().Be("Jane White");
            phoneNumberLine.PhoneNumber.User.Should().Be("+1235325");
            phoneNumberLine.PhoneNumber.Domain.Should().Be("asdf.blub.at");
            phoneNumberLine.PhoneNumber.IsNumeric.Should().BeTrue();
        }
    }
}
