using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Utils;
using SipStack;
using SipStack.Header;
using FluentAssertions;
using Moq;
using System;

namespace SipStackTest.Utils
{
    [TestClass]
    public class ParseResultTest
    {
        private ParseResult<Message> _errorResult;
        private ParseResult<Message> _successResult;

        [TestInitialize]
        public void SetUp()
        {
            _errorResult = new ParseResult<Message>("asdfg");
            var body = new Mock<IBody>();
            var message = new Mock<Message>(new SipStack.Header.Header(new HeaderDto()), body.Object);
            _successResult = new ParseResult<Message>(message.Object);
        }

        [TestMethod]
        public void IsSuccess_Error_false()
        {
            _errorResult.IsSuccess.Should().BeFalse();
        }

        [TestMethod]
        public void IsError_Error_true()
        {
            _errorResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void IsSuccess_NoError_true()
        {
            _successResult.IsSuccess.Should().BeTrue();
        }

        [TestMethod]
        public void IsError_NoError_false()
        {
            _successResult.IsError.Should().BeFalse();
        }

        [TestMethod]
        public void Message_Error_ExceptionThrown()
        {
            Action action = () => { var message = _errorResult.Result; };

            action.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Message_NoError_ExceptionNotThrown()
        {
            Action action = () => { var message = _successResult.Result; };

            action.ShouldNotThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Error_InvalidRequestLine_ParseError()
        {
            _errorResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void ErrorMessage_InvalidRequestLine_asdfg()
        {
            _errorResult.ErrorMessage.Should().Be("asdfg");
        }

        [TestMethod]
        public void ErrorMessage_NoError_ExceptionThrown()
        {
            Action action = () => { var errorMessage = _successResult.ErrorMessage; };

            action.ShouldThrow<InvalidOperationException>();
        }
    }
}
