using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using FluentAssertions;
using SipStack.Header;

namespace SipStackTest.Header
{
    [TestClass]
    public class HeaderFieldParserTest
    {
        private HeaderFieldParser _headerFieldParser;
        private int _end;

        [TestInitialize]
        public void SetUp()
        {
            _headerFieldParser = new HeaderFieldParser();
        }

        [TestMethod]
        public void Parse_EmptyLine_ParseError()
        {
            var result = _headerFieldParser.Parse(new[] { "" }, 0, out _end);

            result.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_RecommendedUsageOfWhiteSpacesWithNameBlub_NameIsBlub()
        {
            var result = _headerFieldParser.Parse(new[] { "blub: heinz" }, 0, out _end);

            result.Result.Name.ToString().Should().Be("blub");
        }

        [TestMethod]
        public void Parse_RecommendedUsageOfWhiteSpacesWithValueHeinz_ValueIsHeinz()
        {
            var result = _headerFieldParser.Parse(new[] { "blub: heinz" }, 0, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("heinz");
        }

        [TestMethod]
        public void Parse_RecommendedUsageOfWhiteSpacesWithNameBlubInLine2_NameIsBlub()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject: lunch", "blub: heinz" }, 1, out _end);

            result.Result.Name.ToString().Should().Be("blub");
        }

        [TestMethod]
        public void Parse_RecommendedUsageOfWhiteSpacesWithValueHeinzInLine2_ValueIsHeinz()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject: lunch", "blub: heinz" }, 1, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("heinz");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionOneWithNameSubject_NameIsSubject()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject:            lunch" }, 0, out _end);

            result.Result.Name.ToString().Should().Be("Subject");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionOneWithValueLunch_ValueIsLunch()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject:            lunch" }, 0, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("lunch");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionTwoWithNameSubject_NameIsSubject()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject      :      lunch" }, 0, out _end);

            result.Result.Name.ToString().Should().Be("Subject");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionTwoWithValueLunch_ValueIsLunch()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject      :      lunch" }, 0, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("lunch");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionThreeWithNameSubject_NameIsSubject()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject            :lunch" }, 0, out _end);

            result.Result.Name.ToString().Should().Be("Subject");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionThreeWithValueLunch_ValueIsLunch()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject            :lunch" }, 0, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("lunch");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionFourWithNameSubject_NameIsSubject()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject:lunch" }, 0, out _end);

            result.Result.Name.ToString().Should().Be("Subject");
        }

        [TestMethod]
        public void Parse_StrangeUsageOfWhiteSpacesVersionFourWithValueLunch_ValueIsLunch()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject:lunch" }, 0, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("lunch");
        }

        [TestMethod]
        public void Parse_ValueInMultipleLinesAndNameSubject_NameIsSubject()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject: I know you're there", "         pick up the phone", "         and talk to me!" }, 0, out _end);

            result.Result.Name.ToString().Should().Be("Subject");
        }

        [TestMethod]
        public void Parse_ValueInMultipleLines_ValueIsCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject: I know you're there", "         pick up the phone", "         and talk to me!" }, 0, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("I know you're there pick up the phone and talk to me!");
        }

        [TestMethod]
        public void Parse_ValueInMultipleLinesAndTabs_ValueIsCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject: I know you're there", "         \tpick up the phone", "\t         and talk to me!" }, 0, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("I know you're there pick up the phone and talk to me!");
        }

        [TestMethod]
        public void Parse_ValueInMultipleLinesAndEmptyFirstLine_ValueIsCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject:", "         \tpick up the phone", "\t         and talk to me!" }, 0, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("pick up the phone and talk to me!");
        }

        [TestMethod]
        public void Parse_ValueInMultipleLinesAndEmptyFirstLineWithMultipleValuesInSecondLine_ValueIsCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Route:", " <sip:alice@atlanta.com>, <sip:bob@biloxi.com>,", "       <sip:carol@chicago.com>" }, 0, out _end);

            result.Result.Values.Count.Should().Be(3);
            result.Result.Values[0].Should().Be("<sip:alice@atlanta.com>");
            result.Result.Values[1].Should().Be("<sip:bob@biloxi.com>");
            result.Result.Values[2].Should().Be("<sip:carol@chicago.com>");
        }

        [TestMethod]
        public void Parse_ValueInMultipleLinesAndOnlyWhitespaceInFirstLine_ValueIsCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject: ", "  \t   \t    pick up the phone", "\t         and talk to me!" }, 0, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("pick up the phone and talk to me!");
        }

        [TestMethod]
        public void Parse_ValueInMultipleLinesAndOnlyWhitespaceInSecondLine_ValueIsCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject: pick up the phone", "  \t   \t    ", "\t         and talk to me!" }, 0, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("pick up the phone  and talk to me!");
        }

        [TestMethod]
        public void Parse_ValueInMultipleLinesAndAnotherField_ValueIsCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Subject: I know you're there", "         pick up the phone", "         and talk to me!", "AnotherField: 123" }, 0, out _end);

            result.Result.Values.Count.Should().Be(1);
            result.Result.Values[0].Should().Be("I know you're there pick up the phone and talk to me!");
        }

        [TestMethod]
        public void Parse_MultipleValuesForRouteInTwoLines_ValuesAreCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Route: <sip:alice@atlanta.com>, <sip:bob@biloxi.com>,", "       <sip:carol@chicago.com>" }, 0, out _end);

            result.Result.Values.Count.Should().Be(3);
            result.Result.Values[0].Should().Be("<sip:alice@atlanta.com>");
            result.Result.Values[1].Should().Be("<sip:bob@biloxi.com>");
            result.Result.Values[2].Should().Be("<sip:carol@chicago.com>");
        }

        [TestMethod]
        public void Parse_MultipleValuesForRouteInTwoLinesWithoutWhitespaces_ValuesAreCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Route: <sip:alice@atlanta.com>,<sip:bob@biloxi.com>,", "       <sip:carol@chicago.com>" }, 0, out _end);

            result.Result.Values.Count.Should().Be(3);
            result.Result.Values[0].Should().Be("<sip:alice@atlanta.com>");
            result.Result.Values[1].Should().Be("<sip:bob@biloxi.com>");
            result.Result.Values[2].Should().Be("<sip:carol@chicago.com>");
        }

        [TestMethod]
        public void Parse_MultipleValuesForRouteInTwoLinesWithWhitespacesBeforeComma_ValuesAreCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Route: <sip:alice@atlanta.com> ,<sip:bob@biloxi.com>,", "       <sip:carol@chicago.com>" }, 0, out _end);

            result.Result.Values.Count.Should().Be(3);
            result.Result.Values[0].Should().Be("<sip:alice@atlanta.com>");
            result.Result.Values[1].Should().Be("<sip:bob@biloxi.com>");
            result.Result.Values[2].Should().Be("<sip:carol@chicago.com>");
        }

        [TestMethod]
        public void Parse_MultipleValuesForRouteInTwoLinesWithWhitespacesBeforeAndAfterComma_ValuesAreCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Route: <sip:alice@atlanta.com> , <sip:bob@biloxi.com> , ", "       <sip:carol@chicago.com>" }, 0, out _end);

            result.Result.Values.Count.Should().Be(3);
            result.Result.Values[0].Should().Be("<sip:alice@atlanta.com>");
            result.Result.Values[1].Should().Be("<sip:bob@biloxi.com>");
            result.Result.Values[2].Should().Be("<sip:carol@chicago.com>");
        }

        [TestMethod]
        public void Parse_MultipleValuesInMultipleLinesWithDotAtTheBeginning_ValuesAreCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "Accept: application/sdp,", ".application/isup" }, 0, out _end);

            result.Result.Values.Count.Should().Be(2);
            result.Result.Values[0].Should().Be("application/sdp");
            result.Result.Values[1].Should().Be("application/isup");
        }

        [TestMethod]
        public void Parse_MultipleValuesForRouteInTwoLines_EndIsCorrect()
        {
            var result = _headerFieldParser.Parse(new[] { "", "", "Route: <sip:alice@atlanta.com> ,<sip:bob@biloxi.com>,", "       <sip:carol@chicago.com>" }, 2, out _end);

            _end.Should().Be(3);
        }
    }
}
