using System.Collections.Generic;
using System.Linq;

namespace SipStack
{
    public enum HeaderFieldType
    {
        To,
        From,
        Contact,
        Route
    }

    public static class HeaderFieldTypeUtils
    {
        private static IDictionary<string, HeaderFieldType> StringToHeaderFieldType;
        private static IDictionary<HeaderFieldType, string> HeaderFieldTypeToString;
        private static HashSet<HeaderFieldType> HeadersWithMultipleValues;

        static HeaderFieldTypeUtils()
        {
            HeaderFieldTypeToString = new Dictionary<HeaderFieldType, string>
            {
                { HeaderFieldType.To, "To" },
                { HeaderFieldType.From, "From" },
                { HeaderFieldType.Contact, "Contact" },
                { HeaderFieldType.Route, "Route" }
            };

            HeadersWithMultipleValues = new HashSet<HeaderFieldType>
            {
                HeaderFieldType.Route
            };

            StringToHeaderFieldType = HeaderFieldTypeToString.ToDictionary(x => x.Value.ToLower(), x => x.Key);

        }

        public static bool TryParse(string value, out HeaderFieldType requestMethod)
        {
            return StringToHeaderFieldType.TryGetValue(value.ToLower(), out requestMethod);
        }

        public static string ToString(this HeaderFieldType value)
        {
            return HeaderFieldTypeToString[value];
        }

        public static bool CanHaveMultipleValues(this HeaderFieldType value)
        {
            return HeadersWithMultipleValues.Contains(value);
        }
    }
}
