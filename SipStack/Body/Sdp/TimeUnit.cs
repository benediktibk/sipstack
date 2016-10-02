namespace SipStack.Body.Sdp
{
    public static class TimeUnit
    {
        public static bool ApplyUnit(long value, string unit, out long result)
        {
            result = 0;
            long multiplicator;

            if (unit == "d")
                multiplicator = 86400;
            else if (unit == "h")
                multiplicator = 3600;
            else if (unit == "m")
                multiplicator = 60;
            else if (string.IsNullOrEmpty(unit) || unit == "s")
                multiplicator = 1;
            else
                return false;

            result = value * multiplicator;
            return true;
        }
    }
}
