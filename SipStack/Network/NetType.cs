using System.Collections.Generic;
using System.Linq;

namespace SipStack.Network
{
    public enum NetType
    {
        Internet
    }

    public static class NetTypeUtils
    {
        private static IDictionary<string, NetType> StringToType;
        private static IDictionary<NetType, string> TypeToString;

        static NetTypeUtils()
        {
            TypeToString = new Dictionary<NetType, string>
            {
                { NetType.Internet, "IN" }
            };

            StringToType = TypeToString.ToDictionary(x => x.Value.ToLower(), x => x.Key);
        }

        public static bool TryParse(string value, out NetType requestMethod)
        {
            return StringToType.TryGetValue(value.ToLower(), out requestMethod);
        }

        public static string ToFriendlyString(this NetType value)
        {
            return TypeToString[value];
        }
    }
}
