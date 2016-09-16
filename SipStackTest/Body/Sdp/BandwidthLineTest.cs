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
        public void CreateFrom_CtBandwidth_TypeAndValueAreCorrect()
        {
            var line = BandwidthLine.CreateFrom("CT:128");

            var bandwidthLine = line.Result as BandwidthLine;
            bandwidthLine.Type.Should().Be(BandwidthType.ConferenceTotal);
            bandwidthLine.Bandwidth.Should().Be(128);
        }

        [TestMethod]
        public void CreateFrom_AsBandwidth_TypeAndValueAreCorrect()
        {
            var line = BandwidthLine.CreateFrom("AS:35");

            var bandwidthLine = line.Result as BandwidthLine;
            bandwidthLine.Type.Should().Be(BandwidthType.ApplicationSpecific);
            bandwidthLine.Bandwidth.Should().Be(35);
        }

        [TestMethod]
        public void CreateFrom_ProprietaryBandwidth_TypeAndValueAreCorrect()
        {
            var line = BandwidthLine.CreateFrom("X-ASDF:59");

            var bandwidthLine = line.Result as BandwidthLine;
            bandwidthLine.Type.Should().Be(BandwidthType.Unknown);
            bandwidthLine.Bandwidth.Should().Be(59);
        }
    }
}
