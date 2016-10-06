using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public enum MediaTransportProtocol
    {
        Udp,
        RtpAvp,
        RtpSavp
    }

    public static class MediaTransportProtocolUtils
    {
        private static IDictionary<string, MediaTransportProtocol> StringToType;
        private static IDictionary<MediaTransportProtocol, string> TypeToString;

        static MediaTransportProtocolUtils()
        {
            TypeToString = new Dictionary<MediaTransportProtocol, string>
            {
                { MediaTransportProtocol.Udp, "udp" },
                { MediaTransportProtocol.RtpAvp, "RTP/AVP" },
                { MediaTransportProtocol.RtpSavp, "RTP/SAVP" }
            };

            StringToType = TypeToString.ToDictionary(x => x.Value.ToLower(), x => x.Key);
        }

        public static bool TryParse(string value, out MediaTransportProtocol parsedValue)
        {
            return StringToType.TryGetValue(value.ToLower(), out parsedValue);
        }

        public static string ToFriendlyString(this MediaTransportProtocol value)
        {
            return TypeToString[value];
        }
    }
}
