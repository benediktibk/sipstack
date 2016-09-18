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
    }
}
