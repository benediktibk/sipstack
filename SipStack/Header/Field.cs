using System;
using System.Collections.Generic;

namespace SipStack.Header
{
    public class HeaderField
    {
        private List<string> _values;

        public HeaderField(FieldName name, IList<string> values)
        {
            if (values == null || values.Count < 1)
                throw new ArgumentException("values", "there must be at least one value");

            Name = name;
            _values = new List<string>(values);
        }

        public FieldName Name { get; private set; }
        public IReadOnlyList<string> Values => _values;

        public void AddTo(MessageBuilder messageBuilder)
        {
            messageBuilder.AddLineFormat("{0}: {1}", Name.ToString(), string.Join(", ", _values));
        }
    }
}
