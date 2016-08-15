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
            var header = Header.CreateFrom(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), new HeaderField[]{ });

            var headerField = header[new HeaderFieldName("asdf")];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("");
        }

        [TestMethod]
        public void IndexerGet_NotSetFromField_EmptyValue()
        {
            var header = Header.CreateFrom(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), new HeaderField[] { });

            var headerField = header[new HeaderFieldName(HeaderFieldType.From)];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("");
        }

        [TestMethod]
        public void IndexerGet_SetField_CorrectValue()
        {
            var source = new HeaderField(new HeaderFieldName("asdf"), new[] { "gdasf" });
            var header = Header.CreateFrom(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), new HeaderField[] { source });

            var headerField = header[new HeaderFieldName("asdf")];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("gdasf");
        }

        [TestMethod]
        public void IndexerGet_SetFrom_CorrectValue()
        {
            var source = new HeaderField(new HeaderFieldName(HeaderFieldType.From), new[] { "gdasf" });
            var header = Header.CreateFrom(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), new HeaderField[] { source });

            var headerField = header[new HeaderFieldName(HeaderFieldType.From)];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("gdasf");
        }

        [TestMethod]
        public void CreateFrom_FieldWithMultipleValues_FieldHasAllValues()
        {
            var headerFields = new[]
            {
                new HeaderField(new HeaderFieldName(HeaderFieldType.Route), new[] {"<sip:alice@atlanta.com>"}),
                new HeaderField(new HeaderFieldName(HeaderFieldType.Route), new[] {"<sip:bob@biloxi.com>", "<sip:carol@chicago.com>"})
            };

            var header = Header.CreateFrom(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), headerFields);

            var routeHeader = header[new HeaderFieldName(HeaderFieldType.Route)];
            routeHeader.Name.Type.Should().Be(HeaderFieldType.Route);
            routeHeader.Values.Count.Should().Be(3);
            routeHeader.Values[0].Should().Be("<sip:alice@atlanta.com>");
            routeHeader.Values[1].Should().Be("<sip:bob@biloxi.com>");
            routeHeader.Values[2].Should().Be("<sip:carol@chicago.com>");
        }

        [TestMethod]
        public void CreateFrom_FieldWithForbiddenMultipleValues_Null()
        {
            var headerFields = new[]
            {
                new HeaderField(new HeaderFieldName(HeaderFieldType.From), new[] {"<sip:alice@atlanta.com>"}),
                new HeaderField(new HeaderFieldName(HeaderFieldType.From), new[] {"<sip:bob@biloxi.com>"})
            };

            var header = Header.CreateFrom(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), headerFields);

            header.Should().BeNull();
        }
    }
}
