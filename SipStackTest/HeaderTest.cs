using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using FluentAssertions;

namespace SipStackTest
{
    [TestClass]
    public class HeaderTest
    {
        [TestMethod]
        public void IndexerGet_NotSetCustomField_EmptyValue()
        {
            var header = new Header(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), new HeaderField[]{ });

            var headerField = header[new HeaderFieldName("asdf")];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("");
        }

        [TestMethod]
        public void IndexerGet_NotSetFromField_EmptyValue()
        {
            var header = new Header(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), new HeaderField[] { });

            var headerField = header[new HeaderFieldName(HeaderFieldType.From)];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("");
        }

        [TestMethod]
        public void IndexerGet_SetField_CorrectValue()
        {
            var source = new HeaderField(new HeaderFieldName("asdf"), new[] { "gdasf" });
            var header = new Header(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), new HeaderField[] { source });

            var headerField = header[new HeaderFieldName("asdf")];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("gdasf");
        }

        [TestMethod]
        public void IndexerGet_SetFrom_CorrectValue()
        {
            var source = new HeaderField(new HeaderFieldName(HeaderFieldType.From), new[] { "gdasf" });
            var header = new Header(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), new HeaderField[] { source });

            var headerField = header[new HeaderFieldName(HeaderFieldType.From)];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("gdasf");
        }
    }
}
