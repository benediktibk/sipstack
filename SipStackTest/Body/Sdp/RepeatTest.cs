using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class RepeatTest
    {
        [TestMethod]
        public void CreateTimeSpanFrom_50s_50s()
        {
            var repeat = Repeat.CreateTimeSpanFrom("50s");
            
            repeat.Result.TotalSeconds.Should().BeApproximately(50, 1e-10);
        }

        [TestMethod]
        public void CreateTimeSpanFrom_40_40s()
        {
            var repeat = Repeat.CreateTimeSpanFrom("40");
            
            repeat.Result.TotalSeconds.Should().BeApproximately(40, 1e-10);
        }

        [TestMethod]
        public void CreateTimeSpanFrom_40m_2400s()
        {
            var repeat = Repeat.CreateTimeSpanFrom("40m");
            
            repeat.Result.TotalSeconds.Should().BeApproximately(2400, 1e-10);
        }

        [TestMethod]
        public void CreateTimeSpanFrom_3h_10800s()
        {
            var repeat = Repeat.CreateTimeSpanFrom("3h");
            
            repeat.Result.TotalSeconds.Should().BeApproximately(10800, 1e-10);
        }

        [TestMethod]
        public void CreateTimeSpanFrom_2d_172800s()
        {
            var repeat = Repeat.CreateTimeSpanFrom("2d");
            
            repeat.Result.TotalSeconds.Should().BeApproximately(172800, 1e-10);
        }

        [TestMethod]
        public void CreateTimeSpanFrom_NegativeValue_Error()
        {
            var repeat = Repeat.CreateTimeSpanFrom("-40");

           repeat.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void CreateTimeSpanFrom_Float_Error()
        {
            var repeat = Repeat.CreateTimeSpanFrom("1.4");

           repeat.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void CreateTimeSpanFrom_40t_Error()
        {
            var repeat = Repeat.CreateTimeSpanFrom("40t");

           repeat.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_ValidRepeatLine_AllValuesAreCorrect()
        {
            var repeat = Repeat.Parse("604800 3600 0 90000");
            
            repeat.Result.RepeatInterval.TotalSeconds.Should().BeApproximately(604800, 1e-10);
            repeat.Result.ActiveDuration.TotalSeconds.Should().BeApproximately(3600, 1e-10);
            repeat.Result.OffsetStart.TotalSeconds.Should().BeApproximately(0, 1e-10);
            repeat.Result.OffsetEnd.TotalSeconds.Should().BeApproximately(90000, 1e-10);
        }

        [TestMethod]
        public void Parse_ValidRepeatLineWithUnits_AllValuesAreCorrect()
        {
            var repeat = Repeat.Parse("7d 1h 3m 25h");
            
            repeat.Result.RepeatInterval.TotalSeconds.Should().BeApproximately(604800, 1e-10);
            repeat.Result.ActiveDuration.TotalSeconds.Should().BeApproximately(3600, 1e-10);
            repeat.Result.OffsetStart.TotalSeconds.Should().BeApproximately(180, 1e-10);
            repeat.Result.OffsetEnd.TotalSeconds.Should().BeApproximately(90000, 1e-10);
        }

        [TestMethod]
        public void Parse_OffsetStartBiggerThanOffsetEnd_Error()
        {
            var repeat = Repeat.Parse("7d 1 3m 25s");

            repeat.IsError.Should().BeTrue();
        }
    }
}
