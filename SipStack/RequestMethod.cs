using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack
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
            { "INVITE", RequestMethod.Invite }
        };

        private static IDictionary<RequestMethod, string> RequestMethodToString = StringToRequestMethod.ToDictionary(x => x.Value, x => x.Key);

        public static RequestMethod Parse(string value)
        {
            try
            {
                return StringToRequestMethod[value];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("value", "invalid SIP request method");
            }
        }

        public static string ToString(RequestMethod value)
        {
            return RequestMethodToString[value];
        }
    }
}
