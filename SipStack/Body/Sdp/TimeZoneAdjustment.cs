namespace SipStack.Body.Sdp
{
    public class TimeZoneAdjustment
    {
        public TimeZoneAdjustment(long time, long offset)
        {
            Time = time;
            Offset = offset;
        }

        public long Time { get; private set; }
        public long Offset { get; private set; }
    }
}
