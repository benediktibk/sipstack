using SipStack.Utils;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class AttributeLine : ILine
    {
        public AttributeLine(string name)
        {
            Name = name;
            Value = "";
        }

        public AttributeLine(string name, string value)
        {
            Name = name;
            Value = value;
        }
        
        public string Name { get; }
        public string Value { get; }

        public static ParseResult<ILine> Parse(string data)
        {
            var pattern = @"^([^:]*):?(.*)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return new ParseResult<ILine>($"attribute line '{data}' is invalid");

            var name = matches[0].Groups[1].Value;
            var value = matches[0].Groups[2].Value;

            return new ParseResult<ILine>(new AttributeLine(name, value));
        }
    }
}
