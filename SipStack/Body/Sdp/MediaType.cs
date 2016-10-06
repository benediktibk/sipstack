using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public enum MediaType
    {
        Audio,
        Video,
        Text,
        Application,
        Message
    }

    public static class MediaTypeUtils
    {
        private static IDictionary<string, MediaType> StringToType;
        private static IDictionary<MediaType, string> TypeToString;

        static MediaTypeUtils()
        {
            TypeToString = new Dictionary<MediaType, string>
            {
                { MediaType.Audio, "audio" },
                { MediaType.Video, "video" },
                { MediaType.Text, "text" },
                { MediaType.Application, "application" },
                { MediaType.Message, "message" }
            };

            StringToType = TypeToString.ToDictionary(x => x.Value.ToLower(), x => x.Key);
        }

        public static bool TryParse(string value, out MediaType parsedValue)
        {
            return StringToType.TryGetValue(value.ToLower(), out parsedValue);
        }

        public static string ToFriendlyString(this MediaType value)
        {
            return TypeToString[value];
        }
    }
}
