using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SipStack.Body.Sdp;
using System;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class TimingTest
    {
        [TestMethod]
        public void Parse_PositiveStartAndEnd_AllValuesAreCorrect()
        {
            var timing = Timing.Parse(@"3122549400 3122550000");
            
            timing.Result.Start.Should().Be(new DateTime(1998, 12, 13, 14, 50, 0));
            timing.Result.End.Should().Be(new DateTime(1998, 12, 13, 15, 00, 0));
        }

        [TestMethod]
        public void Parse_PositiveStartAndZeroEnd_AllValuesAreCorrect()
        {
            var timing = Timing.Parse(@"3122549400 0");
            
            timing.Result.Start.Should().Be(new DateTime(1998, 12, 13, 14, 50, 0));
            timing.Result.End.Should().Be(DateTime.MaxValue);
        }

        [TestMethod]
        public void Parse_NegativeStartAndPositiveEnd_Error()
        {
            var timing = Timing.Parse(@"-2873397496 2873404696");

            timing.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_PositiveStartAndNegativeEnd_Error()
        {
            var timing = Timing.Parse(@"2873397496 -2873404696");

            timing.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_EndBeforeStart_Error()
        {
            var timing = Timing.Parse(@"2973397496 2873404696");

            timing.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_ZeroStartAndEnd_AllValuesAreCorrect()
        {
            var timing = Timing.Parse(@"0 0");
            
            timing.Result.Start.Should().Be(new DateTime(1900, 1, 1, 0, 0, 0));
            timing.Result.End.Ticks.Should().Be(DateTime.MaxValue.Ticks);
        }
    }
}
