using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public enum EncryptionKeyType
    {
        Clear,
        Base64,
        Uri,
        Prompt
    }

    public static class EncryptionKeyTypeUtils
    {
        private static IDictionary<string, EncryptionKeyType> StringToType;
        private static IDictionary<EncryptionKeyType, string> TypeToString;

        static EncryptionKeyTypeUtils()
        {
            TypeToString = new Dictionary<EncryptionKeyType, string>
            {
                { EncryptionKeyType.Clear, "clear" },
                { EncryptionKeyType.Base64, "base64" },
                { EncryptionKeyType.Uri, "uri" },
                { EncryptionKeyType.Prompt, "prompt" }
            };

            StringToType = TypeToString.ToDictionary(x => x.Value.ToLower(), x => x.Key);
        }

        public static bool TryParse(string value, out EncryptionKeyType parsedValue)
        {
            return StringToType.TryGetValue(value.ToLower(), out parsedValue);
        }

        public static string ToFriendlyString(this EncryptionKeyType value)
        {
            return TypeToString[value];
        }
    }
}
