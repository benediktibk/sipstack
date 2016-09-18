using SipStack.Utils;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class AttributeLine : ILine
    {
        private readonly string _name;
        private readonly string _value;

        public AttributeLine(string name)
        {
            _name = name;
            _value = "";
        }

        public AttributeLine(string name, string value)
        {
            _name = name;
            _value = value;
        }
        
        public string Name => _name;
        public string Value => _value;

        public static ParseResult<ILine> CreateFrom(string data)
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
