using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class TimeZoneAdjustmentsTest
    {
        [TestMethod]
        public void Parse_TwoEntry_CorrectValuesForAdjustments()
        {
            var timeZoneAdjustments = TimeZoneAdjustment.Parse(@"2882844526 -1h 2898848070 0");

            var result = timeZoneAdjustments.Result;
            result.Count.Should().Be(2);
            result[0].Time.Should().Be(2882844526L);
            result[0].Offset.Should().Be(-3600);
            result[1].Time.Should().Be(2898848070L);
            result[1].Offset.Should().Be(0);
        }
    }
}
