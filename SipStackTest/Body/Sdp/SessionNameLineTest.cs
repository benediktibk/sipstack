using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class SessionNameLineTest
    {
        [TestMethod]
        public void Parse_ValidName_CorrectName()
        {
            var line = SessionNameLine.Parse("SDP Seminar");

            var sessionNameLine = line.Result as SessionNameLine;
            sessionNameLine.Name.Should().Be("SDP Seminar");
        }

        [TestMethod]
        public void Parse_WhitespaceAsName_CorrectName()
        {
            var line = SessionNameLine.Parse(" ");

            var sessionNameLine = line.Result as SessionNameLine;
            sessionNameLine.Name.Should().Be(" ");
        }

        [TestMethod]
        public void Parse_EmptyName_Error()
        {
            var line = SessionNameLine.Parse("");

            line.IsError.Should().BeTrue();
        }
    }
}
