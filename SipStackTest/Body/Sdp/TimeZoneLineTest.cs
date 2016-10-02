using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class TimeZoneLineTest
    {
        [TestMethod]
        public void Parse_TwoEntry_CorrectValuesForAdjustments()
        {
            var line = TimeZoneLine.Parse(@"2882844526 -1h 2898848070 0");

            var timeZoneLine = line.Result as TimeZoneLine;
            timeZoneLine.TimeZoneAdjustments.Count.Should().Be(2);
            timeZoneLine.TimeZoneAdjustments[0].Time.Should().Be(2882844526L);
            timeZoneLine.TimeZoneAdjustments[0].Offset.Should().Be(-3600);
            timeZoneLine.TimeZoneAdjustments[1].Time.Should().Be(2898848070L);
            timeZoneLine.TimeZoneAdjustments[1].Offset.Should().Be(0);
        }
    }
}
