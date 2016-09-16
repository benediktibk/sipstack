using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public enum BandwidthType
    {
        ConferenceTotal,
        ApplicationSpecific,
        Unknown
    }

    public static class BandwidthTypeUtils
    {
        private static IDictionary<string, BandwidthType> StringToType;
        private static IDictionary<BandwidthType, string> TypeToString;

        static BandwidthTypeUtils()
        {
            TypeToString = new Dictionary<BandwidthType, string>
            {
                { BandwidthType.ConferenceTotal, "CT" },
                { BandwidthType.ApplicationSpecific, "AS" }
            };

            StringToType = TypeToString.ToDictionary(x => x.Value.ToLower(), x => x.Key);
        }

        public static bool TryParse(string value, out BandwidthType requestMethod)
        {
            return StringToType.TryGetValue(value.ToLower(), out requestMethod);
        }

        public static string ToFriendlyString(this BandwidthType value)
        {
            return TypeToString[value];
        }
    }
}