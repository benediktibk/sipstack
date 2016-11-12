using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SipStack.Body.Sdp;
using FluentAssertions;
using System;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class LineWithUsageTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Null_ArgumentNullException()
        {
            var lineWithUsage = new LineWithUsage(null);
        }

        [TestMethod]
        public void Constructor_ValidParameter_NotUsed()
        {
            var line = new Mock<ILine>();
            var lineWithUsage = new LineWithUsage(line.Object);

            lineWithUsage.Used.Should().BeFalse();
        }

        [TestMethod]
        public void MarkAsUsed_NotYetUsed_Used()
        {
            var line = new Mock<ILine>();
            var lineWithUsage = new LineWithUsage(line.Object);

            lineWithUsage.MarkAsUsed();

            lineWithUsage.Used.Should().BeTrue();
        }
    }
}
