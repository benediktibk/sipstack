using SipStack.Utils;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class Attribute
    {
        public Attribute(string name)
        {
            Name = name;
            Value = string.Empty;
        }

        public Attribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }
        public bool IsFlag => string.IsNullOrEmpty(Value);

        public static ParseResult<Attribute> Parse(string data)
        {
            var pattern = @"^([^:]*):?(.*)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return ParseResult<Attribute>.CreateError($"attribute line '{data}' is invalid");

            var name = matches[0].Groups[1].Value;
            var value = matches[0].Groups[2].Value;

            return ParseResult<Attribute>.CreateSuccess(new Attribute(name, value));
        }
    }
}
