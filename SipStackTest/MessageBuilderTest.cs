using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using FluentAssertions;

namespace SipStackTest
{
    [TestClass]
    public class MessageBuilderTest
    {
        private MessageBuilder _messageBuilder;

        [TestInitialize]
        public void SetUp()
        {
            _messageBuilder = new MessageBuilder();
        }

        [TestMethod]
        public void ToString_Empty_EmptyString()
        {
            var result = _messageBuilder.ToString();

            result.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void ToString_TwoLines_NoAdditionalLineEndingAtLastLine()
        {
            _messageBuilder.AddLine("one");
            _messageBuilder.AddLine("two");

            var result = _messageBuilder.ToString();

            result.Should().Be("one\r\ntwo");
        }
    }
}
