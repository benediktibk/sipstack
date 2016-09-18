using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class SessionNameLineTest
    {
        [TestMethod]
        public void CreateFrom_ValidName_CorrectName()
        {
            var line = SessionNameLine.CreateFrom("SDP Seminar");

            var sessionNameLine = line.Result as SessionNameLine;
            sessionNameLine.Name.Should().Be("SDP Seminar");
        }

        [TestMethod]
        public void CreateFrom_WhitespaceAsName_CorrectName()
        {
            var line = SessionNameLine.CreateFrom(" ");

            var sessionNameLine = line.Result as SessionNameLine;
            sessionNameLine.Name.Should().Be(" ");
        }

        [TestMethod]
        public void CreateFrom_EmptyName_Error()
        {
            var line = SessionNameLine.CreateFrom("");

            line.IsError.Should().BeTrue();
        }
    }
}
