using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace SipStack.Network
{
    public enum AddressType
    {
        Ipv4,
        Ipv6
    }

    public static class AddressTypeUtils
    {
        private static IDictionary<string, AddressType> StringToType;
        private static IDictionary<AddressType, string> TypeToString;

        static AddressTypeUtils()
        {
            TypeToString = new Dictionary<AddressType, string>
            {
                { AddressType.Ipv4, "IP4" },
                { AddressType.Ipv6, "IP6" }
            };

            StringToType = TypeToString.ToDictionary(x => x.Value.ToLower(), x => x.Key);
        }

        public static bool TryParse(string value, out AddressType requestMethod)
        {
            return StringToType.TryGetValue(value.ToLower(), out requestMethod);
        }

        public static string ToFriendlyString(this AddressType value)
        {
            return TypeToString[value];
        }

        public static bool Match(AddressType addressType, AddressFamily addressFamily)
        {
            return  (addressType == AddressType.Ipv4 && addressFamily == AddressFamily.InterNetwork) ||
                    (addressType == AddressType.Ipv6 && addressFamily == AddressFamily.InterNetworkV6);
        }
    }
}
