using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;
using FluentAssertions;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class RepeatLineTest
    {
        [TestMethod]
        public void CreateTimeSpanFrom_50s_50s()
        {
            var parseResult = RepeatLine.CreateTimeSpanFrom("50s");

            var timeSpan = parseResult.Result;
            timeSpan.TotalSeconds.Should().BeApproximately(50, 1e-10);
        }

        [TestMethod]
        public void CreateTimeSpanFrom_40_40s()
        {
            var parseResult = RepeatLine.CreateTimeSpanFrom("40");

            var timeSpan = parseResult.Result;
            timeSpan.TotalSeconds.Should().BeApproximately(40, 1e-10);
        }

        [TestMethod]
        public void CreateTimeSpanFrom_40m_2400s()
        {
            var parseResult = RepeatLine.CreateTimeSpanFrom("40m");

            var timeSpan = parseResult.Result;
            timeSpan.TotalSeconds.Should().BeApproximately(2400, 1e-10);
        }

        [TestMethod]
        public void CreateTimeSpanFrom_3h_10800s()
        {
            var parseResult = RepeatLine.CreateTimeSpanFrom("3h");

            var timeSpan = parseResult.Result;
            timeSpan.TotalSeconds.Should().BeApproximately(10800, 1e-10);
        }

        [TestMethod]
        public void CreateTimeSpanFrom_2d_172800s()
        {
            var parseResult = RepeatLine.CreateTimeSpanFrom("2d");

            var timeSpan = parseResult.Result;
            timeSpan.TotalSeconds.Should().BeApproximately(172800, 1e-10);
        }

        [TestMethod]
        public void CreateTimeSpanFrom_NegativeValue_Error()
        {
            var parseResult = RepeatLine.CreateTimeSpanFrom("-40");

            parseResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void CreateTimeSpanFrom_Float_Error()
        {
            var parseResult = RepeatLine.CreateTimeSpanFrom("1.4");

            parseResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void CreateTimeSpanFrom_40t_Error()
        {
            var parseResult = RepeatLine.CreateTimeSpanFrom("40t");

            parseResult.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_ValidRepeatLine_AllValuesAreCorrect()
        {
            var line = RepeatLine.Parse("604800 3600 0 90000");

            var repeatLine = line.Result as RepeatLine;
            repeatLine.Repeat.RepeatInterval.TotalSeconds.Should().BeApproximately(604800, 1e-10);
            repeatLine.Repeat.ActiveDuration.TotalSeconds.Should().BeApproximately(3600, 1e-10);
            repeatLine.Repeat.OffsetStart.TotalSeconds.Should().BeApproximately(0, 1e-10);
            repeatLine.Repeat.OffsetEnd.TotalSeconds.Should().BeApproximately(90000, 1e-10);
        }

        [TestMethod]
        public void Parse_ValidRepeatLineWithUnits_AllValuesAreCorrect()
        {
            var line = RepeatLine.Parse("7d 1h 3m 25h");

            var repeatLine = line.Result as RepeatLine;
            repeatLine.Repeat.RepeatInterval.TotalSeconds.Should().BeApproximately(604800, 1e-10);
            repeatLine.Repeat.ActiveDuration.TotalSeconds.Should().BeApproximately(3600, 1e-10);
            repeatLine.Repeat.OffsetStart.TotalSeconds.Should().BeApproximately(180, 1e-10);
            repeatLine.Repeat.OffsetEnd.TotalSeconds.Should().BeApproximately(90000, 1e-10);
        }

        [TestMethod]
        public void Parse_OffsetStartBiggerThanOffsetEnd_Error()
        {
            var line = RepeatLine.Parse("7d 1 3m 25s");

            line.IsError.Should().BeTrue();
        }
    }
}
