using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Utils;
using FluentAssertions;

namespace SipStackTest.Utils
{
    [TestClass]
    public class PhoneNumberTest
    {
        [TestMethod]
        public void Parse_E164Number_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+43348798431");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("+43348798431");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_E164NumberWithHyphenAndWhitespaces_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+1 617 555-6011");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_E164NumberWithSlashAndHyphen_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+1/617/555-6011");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_OnlyExtension_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("6011");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("6011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_E164NumberWithDisplayNameBefore_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("Sepp Huber <+43348798431>");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("+43348798431");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_E164NumberWithHyphenAndWhitespacesAndDisplayNameBefore_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("Sepp Huber <+1 617 555-6011>");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_E164NumberWithSlashAndHyphenAndDisplayNameBefore_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("Sepp Huber <+1/617/555-6011>");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_OnlyExtensionAndDisplayNameBefore_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("Sepp Huber <6011>");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("6011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_E164NumberWithDisplayNameAfter_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+43348798431 (Sepp Huber)");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("+43348798431");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_E164NumberWithHyphenAndWhitespacesAndDisplayNameAfter_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+1 617 555-6011 (Sepp Huber)");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_E164NumberWithSlashAndHyphenAndDisplayNameAfter_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+1/617/555-6011 (Sepp Huber)");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }

        [TestMethod]
        public void Parse_OnlyExtensionAndDisplayNameAfter_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("6011 (Sepp Huber)");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("");
            phoneNumber.User.Should().Be("6011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeFalse();
        }
        
        [TestMethod]
        public void Parse_E164NumberWithDomain_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+43348798431@asdf.example.com");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("+43348798431");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_E164NumberWithDomainAndHyphenAndWhitespaces_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+1 617 555-6011@asdf.example.com");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_E164NumberWithDomainSlashAndHyphen_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+1/617/555-6011@asdf.example.com");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_OnlyExtensionWithDomain_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("6011@asdf.example.com");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("6011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_E164NumberWithAndDomainDisplayNameBefore_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("Sepp Huber <+43348798431@asdf.example.com>");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("+43348798431");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_E164NumberWithDomainAndHyphenAndWhitespacesAndDisplayNameBefore_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("Sepp Huber <+1 617 555-6011@asdf.example.com>");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_E164NumberWithDomainAndSlashAndHyphenAndDisplayNameBefore_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("Sepp Huber <+1/617/555-6011@asdf.example.com>");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_OnlyExtensionWithDomainAndDisplayNameBefore_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("Sepp Huber <6011@asdf.example.com>");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("6011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_E164NumberWithDomainAndDisplayNameAfter_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+43348798431@asdf.example.com (Sepp Huber)");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("+43348798431");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_E164NumberWithDomainAndHyphenAndWhitespacesAndDisplayNameAfter_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+1 617 555-6011@asdf.example.com (Sepp Huber)");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_E164NumberWithDomainAndSlashAndHyphenAndDisplayNameAfter_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("+1/617/555-6011@asdf.example.com (Sepp Huber)");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("+16175556011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_OnlyExtensionWithDomainAndDisplayNameAfter_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("6011@asdf.example.com (Sepp Huber)");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("6011");
            phoneNumber.IsAlphaNumeric.Should().BeFalse();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_Uri_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("sepp.huber@asdf.example.com");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("sepp.huber");
            phoneNumber.IsAlphaNumeric.Should().BeTrue();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_UriWithTwoAts_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("sepp@huber@asdf.example.com");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("sepp@huber");
            phoneNumber.IsAlphaNumeric.Should().BeTrue();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_UriWithDisplayNameAfter_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("sepp.huber@asdf.example.com (Sepp Huber)");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("sepp.huber");
            phoneNumber.IsAlphaNumeric.Should().BeTrue();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_UriWithTwoAtsAndDisplayNameAfter_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("sepp@huber@asdf.example.com (Sepp Huber)");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("sepp@huber");
            phoneNumber.IsAlphaNumeric.Should().BeTrue();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_UriWithDisplayNameBefore_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("Sepp Huber <sepp.huber@asdf.example.com>");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("sepp.huber");
            phoneNumber.IsAlphaNumeric.Should().BeTrue();
            phoneNumber.HasDomain.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_UriWithTwoAtsAndDisplayNameBefore_AllValuesAreCorrect()
        {
            var parseResult = PhoneNumber.Parse("Sepp Huber <sepp@huber@asdf.example.com>");

            var phoneNumber = parseResult.Result;
            phoneNumber.DisplayName.Should().Be("Sepp Huber");
            phoneNumber.Domain.Should().Be("asdf.example.com");
            phoneNumber.User.Should().Be("sepp@huber");
            phoneNumber.IsAlphaNumeric.Should().BeTrue();
            phoneNumber.HasDomain.Should().BeTrue();
        }
    }
}
