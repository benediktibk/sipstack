using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class BandwidthLineTest
    {
        [TestMethod]
        public void Parse_CtBandwidth_TypeAndValueAreCorrect()
        {
            var line = BandwidthLine.Parse("CT:128");

            var bandwidthLine = line.Result as BandwidthLine;
            bandwidthLine.Bandwidth.Type.Should().Be(BandwidthType.ConferenceTotal);
            bandwidthLine.Bandwidth.Amount.Should().Be(128);
        }

        [TestMethod]
        public void Parse_AsBandwidth_TypeAndValueAreCorrect()
        {
            var line = BandwidthLine.Parse("AS:35");

            var bandwidthLine = line.Result as BandwidthLine;
            bandwidthLine.Bandwidth.Type.Should().Be(BandwidthType.ApplicationSpecific);
            bandwidthLine.Bandwidth.Amount.Should().Be(35);
        }

        [TestMethod]
        public void Parse_ProprietaryBandwidth_TypeAndValueAreCorrect()
        {
            var line = BandwidthLine.Parse("X-ASDF:59");

            var bandwidthLine = line.Result as BandwidthLine;
            bandwidthLine.Bandwidth.Type.Should().Be(BandwidthType.Unknown);
            bandwidthLine.Bandwidth.Amount.Should().Be(59);
        }
    }
}
