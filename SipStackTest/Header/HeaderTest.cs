﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SipStack.Header;

namespace SipStackTest.Header
{
    [TestClass]
    public class HeaderTest
    {
        [TestMethod]
        public void IndexerGet_NotSetCustomField_EmptyValue()
        {
            var headerFields = new[]
            {
                new HeaderField(new HeaderFieldName("asdf"), new[] { "gdasf" }),
                new HeaderField(new HeaderFieldName(HeaderFieldType.ContentLength), new[] {"0"})
            };
            var header = SipStack.Header.Header.Parse(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), headerFields);

            var headerField = header.Result[new HeaderFieldName("blub")];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("");
        }

        [TestMethod]
        public void IndexerGet_NotSetFromField_EmptyValue()
        {
            var headerFields = new[]
            {
                new HeaderField(new HeaderFieldName("asdf"), new[] { "gdasf" }),
                new HeaderField(new HeaderFieldName(HeaderFieldType.ContentLength), new[] {"0"})
            };
            var header = SipStack.Header.Header.Parse(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), headerFields);

            var headerField = header.Result[new HeaderFieldName(HeaderFieldType.From)];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("");
        }

        [TestMethod]
        public void IndexerGet_SetField_CorrectValue()
        {
            var headerFields = new[]
            {
                new HeaderField(new HeaderFieldName("asdf"), new[] { "gdasf" }),
                new HeaderField(new HeaderFieldName(HeaderFieldType.ContentLength), new[] {"0"})
            };
            var header = SipStack.Header.Header.Parse(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), headerFields);

            var headerField = header.Result[new HeaderFieldName("asdf")];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("gdasf");
        }

        [TestMethod]
        public void IndexerGet_SetFrom_CorrectValue()
        {
            var headerFields = new[]
            {
                new HeaderField(new HeaderFieldName(HeaderFieldType.From), new[] { "gdasf" }),
                new HeaderField(new HeaderFieldName(HeaderFieldType.ContentLength), new[] {"0"})
            };
            var header = SipStack.Header.Header.Parse(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), headerFields);

            var headerField = header.Result[new HeaderFieldName(HeaderFieldType.From)];

            headerField.Values.Count.Should().Be(1);
            headerField.Values[0].Should().Be("gdasf");
        }

        [TestMethod]
        public void Parse_FieldWithMultipleValues_FieldHasAllValues()
        {
            var headerFields = new[]
            {
                new HeaderField(new HeaderFieldName(HeaderFieldType.Route), new[] {"<sip:alice@atlanta.com>"}),
                new HeaderField(new HeaderFieldName(HeaderFieldType.Route), new[] {"<sip:bob@biloxi.com>", "<sip:carol@chicago.com>"}),
                new HeaderField(new HeaderFieldName(HeaderFieldType.ContentLength), new[] {"0"})
            };

            var header = SipStack.Header.Header.Parse(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), headerFields);

            var routeHeader = header.Result[new HeaderFieldName(HeaderFieldType.Route)];
            routeHeader.Name.Type.Should().Be(HeaderFieldType.Route);
            routeHeader.Values.Count.Should().Be(3);
            routeHeader.Values[0].Should().Be("<sip:alice@atlanta.com>");
            routeHeader.Values[1].Should().Be("<sip:bob@biloxi.com>");
            routeHeader.Values[2].Should().Be("<sip:carol@chicago.com>");
        }

        [TestMethod]
        public void Parse_FieldWithForbiddenMultipleValues_Error()
        {
            var headerFields = new[]
            {
                new HeaderField(new HeaderFieldName(HeaderFieldType.From), new[] {"<sip:alice@atlanta.com>"}),
                new HeaderField(new HeaderFieldName(HeaderFieldType.From), new[] {"<sip:bob@biloxi.com>"}),
                new HeaderField(new HeaderFieldName(HeaderFieldType.ContentLength), new[] {"0"})
            };

            var header = SipStack.Header.Header.Parse(new RequestLine(RequestMethod.Invite, "ASDFASDFGASDG"), headerFields);

            header.IsError.Should().BeTrue();
        }
    }
}
