﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SipStack.Body;

namespace SipStackTest.Body
{
    [TestClass]
    public class NoBodyParserTest
    {
        private NoBodyParser _noBodyParser;

        [TestInitialize]
        public void SetUp()
        {
            _noBodyParser = new NoBodyParser();
        }

        [TestMethod]
        public void Parse_NullValues_NoBody()
        {
            var result = _noBodyParser.Parse(null, 0, 0);

            result.Result.Should().BeOfType(typeof(NoBody));
        }
    }
}
