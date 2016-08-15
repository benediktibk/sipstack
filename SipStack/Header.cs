using System;
using System.Collections.Generic;

namespace SipStack
{
    public class Header : IReadOnlyHeader
    {
        private IDictionary<HeaderFieldName, HeaderField> _fieldsByType;

        public Header()
        {
            _fieldsByType = new Dictionary<HeaderFieldName, HeaderField>();
        }

        public IMethod Method { get; set; }
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
            set
            {
                if (!value.Name.Equals(fieldName))
                    throw new ArgumentException("parameter fieldname does not match fieldname of value");

                _fieldsByType[fieldName] = value;
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
