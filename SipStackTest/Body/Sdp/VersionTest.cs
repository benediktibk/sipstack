using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class VersionTest
    {
        [TestMethod]
        public void Parse_0_Version0()
        {
            var version = Version.Parse("0");

            version.Result.Value.Should().Be(0);
        }

        [TestMethod]
        public void Parse_5_ParseError()
        {
            var version = Version.Parse("5");

            version.IsError.Should().BeTrue();
        }
    }
}
