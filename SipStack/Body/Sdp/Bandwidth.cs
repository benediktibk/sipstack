namespace SipStack.Body.Sdp
{
    public class Bandwidth
    {
        public Bandwidth(BandwidthType type, int bandwidth)
        {
            Type = type;
            Amount = bandwidth;
        }

        public BandwidthType Type { get; }
        public int Amount { get; }
    }
}
