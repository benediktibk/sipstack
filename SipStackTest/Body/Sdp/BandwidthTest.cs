using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class BandwidthTest
    {
        [TestMethod]
        public void Parse_CtBandwidth_TypeAndValueAreCorrect()
        {
            var bandwidth = Bandwidth.Parse("CT:128");

            bandwidth.Result.Type.Should().Be(BandwidthType.ConferenceTotal);
            bandwidth.Result.Amount.Should().Be(128);
        }

        [TestMethod]
        public void Parse_AsBandwidth_TypeAndValueAreCorrect()
        {
            var bandwidth = Bandwidth.Parse("AS:35");
            
            bandwidth.Result.Type.Should().Be(BandwidthType.ApplicationSpecific);
            bandwidth.Result.Amount.Should().Be(35);
        }

        [TestMethod]
        public void Parse_ProprietaryBandwidth_TypeAndValueAreCorrect()
        {
            var bandwidth = Bandwidth.Parse("X-ASDF:59");
            
            bandwidth.Result.Type.Should().Be(BandwidthType.Unknown);
            bandwidth.Result.Amount.Should().Be(59);
        }
    }
}
