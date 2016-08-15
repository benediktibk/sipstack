using System.Collections.Generic;
using System.Text;

namespace SipStack
{
    public class HeaderField
    {
        private List<string> _values;

        public HeaderField(HeaderFieldName name, IList<string> values)
        {
            Name = name;
            _values = new List<string>(values);
        }

        public HeaderFieldName Name { get; private set; }
        public IReadOnlyList<string> Values => _values;

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("{0}: {1}", Name.ToString(), string.Join(", ", _values));
            return stringBuilder.ToString();
        }
    }
}
