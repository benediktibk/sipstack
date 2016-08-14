using System.Collections.Generic;

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
    }
}
