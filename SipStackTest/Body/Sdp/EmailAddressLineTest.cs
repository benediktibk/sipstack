using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Utils;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class EmailAddressLineTest
    {
        [TestMethod]
        public void CreateFrom_ValidEmailAddress_AllValuesAreCorrect()
        {
            var line = EmailAddressLine.CreateFrom(@"j.doe@example.com (Jane Doe)");

            var attributeLine = line.Result as AttributeLine;
            attributeLine.Name.Should().Be("recvonly");
        }
    }
}
