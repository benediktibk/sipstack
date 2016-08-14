using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using FluentAssertions;

namespace SipStackTest
{
    [TestClass]
    public class HeaderFieldParserTest
    {
        private HeaderFieldParser _headerFieldParser;

        [TestInitialize]
        public void SetUp()
        {
            _headerFieldParser = new HeaderFieldParser();
        }

        [TestMethod]
        public void Parse_EmptyLine_InvalidHeaderField()
        {
            var result = _headerFieldParser.Parse("");

            result.Error.Should().Be(ParseError.InvalidHeaderField);
        }

        [TestMethod]
        public void Parse_RecommendedUsageOfWhiteSpacesWithNameBlub_NameIsBlub()
        {
            var result = _headerFieldParser.Parse("blub: heinz");

            result.Result.Name.Should().Be("blub");
        }

        [TestMethod]
        public void Parse_RecommendedUsageOfWhiteSpacesWithValueHeinz_ValueIsHeinz()
        {
            var result = _headerFieldParser.Parse("blub: heinz");

            result.Result.Value.Should().Be("heinz");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionOneWithNameSubject_NameIsSubject()
        {
            var result = _headerFieldParser.Parse("Subject:            lunch");

            result.Result.Name.Should().Be("Subject");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionOneWithValueLunch_ValueIsLunch()
        {
            var result = _headerFieldParser.Parse("Subject:            lunch");

            result.Result.Value.Should().Be("lunch");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionTwoWithNameSubject_NameIsSubject()
        {
            var result = _headerFieldParser.Parse("Subject      :      lunch");

            result.Result.Name.Should().Be("Subject");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionTwoWithValueLunch_ValueIsLunch()
        {
            var result = _headerFieldParser.Parse("Subject      :      lunch");

            result.Result.Value.Should().Be("lunch");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionThreeWithNameSubject_NameIsSubject()
        {
            var result = _headerFieldParser.Parse("Subject            :lunch");

            result.Result.Name.Should().Be("Subject");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionThreeWithValueLunch_ValueIsLunch()
        {
            var result = _headerFieldParser.Parse("Subject            :lunch");

            result.Result.Value.Should().Be("lunch");
        }
    }
}
