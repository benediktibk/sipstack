using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class VersionLineTest
    {
        [TestMethod]
        public void Parse_0_Version0()
        {
            var line = VersionLine.Parse("0");

            var versionLine = line.Result as VersionLine;
            versionLine.Version.Should().Be(0);
        }

        [TestMethod]
        public void Parse_5_ParseError()
        {
            var line = VersionLine.Parse("5");

            line.IsError.Should().BeTrue();
        }
    }
}
