using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Utils;
using FluentAssertions;

namespace SipStackTest.Utils
{
    [TestClass]
    public class DateTimeHelperTest
    {
        [TestMethod]
        public void NtpTimeStampToDateTime_ValidTimeStamp_CorrectResult()
        {
            var timeStamp = 3122550000;

            var dateTime = DateTimeHelper.NtpTimeStampToDateTime(timeStamp);

            dateTime.Should().Be(new DateTime(1998, 12, 13, 15, 00, 0, DateTimeKind.Utc));
        }

        [TestMethod]
        public void DateTimeToNtpTimeStamp_ValidDateTime_CorrectResult()
        {
            var dateTime = new DateTime(1998, 12, 13, 15, 00, 0, DateTimeKind.Utc);

            var timeStamp = DateTimeHelper.DateTimeToNtpTimeStamp(dateTime);

            timeStamp.Should().Be(3122550000);
        }
    }
}
