using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SipStack.Body;
using SipStack.Body.Sdp;

namespace SipStackTest.Body
{
    [TestClass]
    public class BodyParserFactoryTest
    {
        private BodyParserFactory _bodyParserFactory;

        [TestInitialize]
        public void SetUp()
        {
            _bodyParserFactory = new BodyParserFactory();
        }

        [TestMethod]
        public void Create_EmptyString_NoBodyParser()
        {
            var result = _bodyParserFactory.Create("");

            result.Should().BeOfType(typeof(NoBodyParser));
        }

        [TestMethod]
        public void Create_Null_NoBodyParser()
        {
            var result = _bodyParserFactory.Create(null);

            result.Should().BeOfType(typeof(NoBodyParser));
        }

        [TestMethod]
        public void Create_SdpInUsualForm_SdpBodyParser()
        {
            var result = _bodyParserFactory.Create("application/sdp");

            result.Should().BeOfType(typeof(SdpBodyParser));
        }

        [TestMethod]
        public void Create_SdpWithStrangeCases_SdpBodyParser()
        {
            var result = _bodyParserFactory.Create("applIcation/SDP");

            result.Should().BeOfType(typeof(SdpBodyParser));
        }
    }
}
