using System.Collections.Generic;

namespace SipStack
{
    public class Header : IReadOnlyHeader
    {
        private IDictionary<HeaderFieldType, HeaderField> _fieldsByType;
        private IDictionary<string, HeaderField> _fieldsByName;

        public Header(IMethod method)
        {
            Method = method;
            _fieldsByType = new Dictionary<HeaderFieldType, HeaderField>();
            _fieldsByName = new Dictionary<string, HeaderField>();
        }

        public IMethod Method { get; private set; }
        public int ContentLength => GetIntegerByType(HeaderFieldType.MaxForwards);

        public HeaderField this[HeaderFieldName fieldName]
        {
            get
            {
                HeaderField result;

                if (fieldName.IsCustomField)
                {
                    if (_fieldsByName.TryGetValue(fieldName.ToString(), out result))
                        return result;
                }
                else
                {
                    if (_fieldsByType.TryGetValue(fieldName.Type, out result))
                        return result;
                }

                return new HeaderField(fieldName, new[] { "" });
            }
            set
            {
                if (fieldName.IsCustomField)
                    _fieldsByName[fieldName.ToString()] = value;
                else
                    _fieldsByType[fieldName.Type] = value;
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
