using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.IO;
using System.Collections.Generic;
using SipStack.Body.Sdp;
using System;

namespace SipStackTest.Body
{ 
    [TestClass]
    public class SdpBodyParserTest
    {
        private SdpBodyParser _sdpBodyParser;

        [TestInitialize]
        public void SetUp()
        {
            _sdpBodyParser = new SdpBodyParser(new LineParser());
        }

        [TestMethod]
        public void Parse_ValidBody_SdpBody()
        {
            var lines = ReadFromFile("005_sdp");
            var result = _sdpBodyParser.Parse(lines, 4, 27);

            result.Result.Should().BeOfType(typeof(SdpBody));
        }

        private static IList<string> ReadFromFile(string file)
        {
            var content = File.ReadAllText($"messages/{file}.txt");
            return content.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        }
    }
}
