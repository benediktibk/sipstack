using System.Collections.Generic;

namespace SipStack.Header
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

        public void AddTo(MessageBuilder messageBuilder)
        {
            messageBuilder.AddLineFormat("{0}: {1}", Name.ToString(), string.Join(", ", _values));
        }
    }
}
