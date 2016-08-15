using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack
{
    public class Header
    {
        private IDictionary<HeaderFieldName, HeaderField> _fieldsByType;

        public Header(IMethod method, IList<HeaderField> fields)
        {
            _fieldsByType = new Dictionary<HeaderFieldName, HeaderField>();

            foreach (var field in fields)
                _fieldsByType[field.Name] = field;
        }

        public IMethod Method { get; }
        public int ContentLength => GetIntegerByType(HeaderFieldType.ContentLength);

        public HeaderField this[HeaderFieldName fieldName]
        {
            get
            {
                HeaderField result;

                if (_fieldsByType.TryGetValue(fieldName, out result))
                    return result;

                return new HeaderField(fieldName, new[] { "" });
            }
        }

        private int GetIntegerByType(HeaderFieldType type)
        {
            var field = this[new HeaderFieldName(type)];
            var value = field.Values[0];
            return int.Parse(value);
        }
    }
}
