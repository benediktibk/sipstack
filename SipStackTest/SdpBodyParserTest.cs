using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack;
using FluentAssertions;
using System.IO;
using System.Collections.Generic;

namespace SipStackTest
{ 
    [TestClass]
    public class SdpBodyParserTest
    {
        private SdpBodyParser _sdpBodyParser;

        [TestInitialize]
        public void SetUp()
        {
            _sdpBodyParser = new SdpBodyParser();
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
            return content.Split('\n');
        }
    }
}
