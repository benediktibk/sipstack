using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;
using System;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class UriLineTest
    {
        [TestMethod]
        public void Parse_ValidUrl_UrlIsCorrect()
        {
            var line = UriLine.Parse(@"http://aasdf.fdasf.dt");

            var uriLine = line.Result as UriLine;
            uriLine.Uri.Should().Be(new Uri(@"http://aasdf.fdasf.dt"));
        }

        [TestMethod]
        public void Parse_InvalidUrl_ParseError()
        {
            var line = UriLine.Parse(@"asdf@asdf.@@asdf");

            line.IsError.Should().BeTrue();
        }
    }
}
