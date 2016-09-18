using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using SipStack.Body.Sdp;
using System;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class TimeLineTest
    {
        [TestMethod]
        public void CreateFrom_PositiveStartAndEnd_AllValuesAreCorrect()
        {
            var line = TimeLine.CreateFrom(@"2873397496 2873404696");

            var timeLine = line.Result as TimeLine;
            timeLine.Start.Ticks.Should().Be(2873397496L);
            timeLine.End.Ticks.Should().Be(2873404696L);
        }

        [TestMethod]
        public void CreateFrom_PositiveStartAndZeroEnd_AllValuesAreCorrect()
        {
            var line = TimeLine.CreateFrom(@"2873397496 0");

            var timeLine = line.Result as TimeLine;
            timeLine.Start.Ticks.Should().Be(2873397496L);
            timeLine.End.Ticks.Should().Be(DateTime.MaxValue.Ticks);
        }

        [TestMethod]
        public void CreateFrom_NegativeStartAndPositiveEnd_Error()
        {
            var line = TimeLine.CreateFrom(@"-2873397496 2873404696");

            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void CreateFrom_PositiveStartAndNegativeEnd_Error()
        {
            var line = TimeLine.CreateFrom(@"2873397496 -2873404696");

            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void CreateFrom_EndBeforeStart_Error()
        {
            var line = TimeLine.CreateFrom(@"2973397496 2873404696");

            line.IsError.Should().BeTrue();
        }
    }
}
