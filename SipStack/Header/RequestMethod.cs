using SipStack.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Header
{
    public enum RequestMethod
    {
        Invite,
        Ack,
        Bye,
        Cancel,
        Options,
        Register,
        Prack,
        Subscribe,
        Notify,
        Publish,
        Info,
        Refer,
        Message,
        Update
    }

    public static class RequestMethodUtils
    {
        private static IDictionary<string, RequestMethod> StringToRequestMethod = new Dictionary<string, RequestMethod>
        {
            { "INVITE", RequestMethod.Invite },
            { "ACK", RequestMethod.Ack },
            { "BYE", RequestMethod.Bye },
            { "CANCEL", RequestMethod.Cancel },
            { "OPTIONS", RequestMethod.Options },
            { "REGISTER", RequestMethod.Register },
            { "PRACK", RequestMethod.Prack },
            { "SUBSCRIBE", RequestMethod.Subscribe },
            { "NOTIFY", RequestMethod.Notify },
            { "PUBLISH", RequestMethod.Publish },
            { "INFO", RequestMethod.Info },
            { "REFER", RequestMethod.Refer },
            { "MESSAGE", RequestMethod.Message },
            { "UPDATE", RequestMethod.Update }
        };

        private static IDictionary<RequestMethod, string> RequestMethodToString = StringToRequestMethod.ToDictionary(x => x.Value, x => x.Key);

        public static bool TryParse(string value, out RequestMethod requestMethod)
        {
            return StringToRequestMethod.TryGetValue(value, out requestMethod);
        }

        public static string ToFriendlyString(this RequestMethod value)
        {
            return RequestMethodToString[value];
        }

        public static ParseResult<RequestMethod> Parse(string value)
        {
            RequestMethod result;

            if (!TryParse(value, out result))
                return new ParseResult<RequestMethod>($"could not parse {value} to RequestMethod");

            return new ParseResult<RequestMethod>(result);
        }
    }
}
