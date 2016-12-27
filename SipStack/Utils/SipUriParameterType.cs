using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack.Utils
{
    public enum SipUriParameterType
    {
        Transport,
        Maddr,
        Ttl,
        User,
        Method,
        Lr
    }
    public static class SipUriParameterTypeUtils
    {
        private static IDictionary<string, SipUriParameterType> StringToValue = new Dictionary<string, SipUriParameterType>
        {
            { "transport", SipUriParameterType.Transport },
            { "maddr", SipUriParameterType.Maddr },
            { "ttl", SipUriParameterType.Ttl },
            { "user", SipUriParameterType.User },
            { "method", SipUriParameterType.Method },
            { "lr", SipUriParameterType.Lr }
        };

        private static IDictionary<SipUriParameterType, string> ValueToString = StringToValue.ToDictionary(x => x.Value, x => x.Key);

        public static bool TryParse(string value, out SipUriParameterType requestMethod)
        {
            return StringToValue.TryGetValue(value, out requestMethod);
        }

        public static string ToFriendlyString(this SipUriParameterType value)
        {
            return ValueToString[value];
        }

        public static ParseResult<SipUriParameterType> Parse(string value)
        {
            SipUriParameterType result;

            if (!TryParse(value, out result))
                return ParseResult<SipUriParameterType>.CreateError($"could not parse {value} to RequestMethod");

            return ParseResult<SipUriParameterType>.CreateSuccess(result);
        }
    }
}
