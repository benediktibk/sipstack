using System;

namespace SipStack.Utils
{
    public class DateTimeHelper
    {
        private static DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private static long _offsetFromNtpToUnix = 2208988800;

        public static DateTime NtpTimeStampToDateTime(long timeStamp)
        {
            return _unixEpoch.AddSeconds(timeStamp - _offsetFromNtpToUnix).ToUniversalTime();
        }

        public static long DateTimeToNtpTimeStamp(DateTime dateTime)
        {
            var differenceToUnixEpoch = dateTime - _unixEpoch;
            return (long)(differenceToUnixEpoch.TotalSeconds) + _offsetFromNtpToUnix;
        }
    }
}
