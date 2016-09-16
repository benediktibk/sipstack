using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class DescriptionLineTest
    {
        [TestMethod]
        public void CreateFrom_validDescription_DescriptionIsCorrect()
        {
            var line = DescriptionLine.CreateFrom("asdf sadf . : asdf?");

            var descriptionLine = line.Result as DescriptionLine;
            descriptionLine.Description.Should().Be("asdf sadf . : asdf?");
        }
    }
}
