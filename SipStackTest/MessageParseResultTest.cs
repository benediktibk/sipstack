using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using FluentAssertions;
using Moq;
using System;

namespace SipStackTest
{
    [TestClass]
    public class MessageParseResultTest
    {
        private MessageParseResult _errorResult;
        private MessageParseResult _successResult;

        [TestInitialize]
        public void SetUp()
        {
            _errorResult = new MessageParseResult(MessageParseError.InvalidRequestLine, "asdfg");
            var message = new Mock<Message>(null, null);
            _successResult = new MessageParseResult(message.Object);
        }

        [TestMethod]
        public void Constructor_ParseErrorNone_ThrowsException()
        {
            Action action = () => { var result = new MessageParseResult(MessageParseError.None, ""); };

            action.ShouldThrow<ArgumentException>();
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
            Action action = () => { var message = _errorResult.Message; };

            action.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Message_NoError_ExceptionNotThrown()
        {
            Action action = () => { var message = _successResult.Message; };

            action.ShouldNotThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void Error_InvalidRequestLine_InvalidRequestLine()
        {
            _errorResult.Error.Should().Be(MessageParseError.InvalidRequestLine);
        }

        [TestMethod]
        public void Error_NoError_ExceptionThrown()
        {
            Action action = () => { var error = _successResult.Error; };

            action.ShouldThrow<InvalidOperationException>();
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
