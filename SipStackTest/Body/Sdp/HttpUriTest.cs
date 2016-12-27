using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;
using System;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class HttpUriTest
    {
        [TestMethod]
        public void Parse_ValidUrl_UrlIsCorrect()
        {
            var uriLine = HttpUri.Parse(@"http://aasdf.fdasf.dt");
            
            uriLine.Result.Uri.Should().Be(new Uri(@"http://aasdf.fdasf.dt"));
        }

        [TestMethod]
        public void Parse_InvalidUrl_ParseError()
        {
            var uriLine = HttpUri.Parse(@"asdf@asdf.@@asdf");

            uriLine.IsError.Should().BeTrue();
        }
    }
}
