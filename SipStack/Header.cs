using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack
{
    public class Header
    {
        private IDictionary<HeaderFieldName, HeaderField> _fieldsByType;

        private Header(IMethod method, IDictionary<HeaderFieldName, HeaderField> fields)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            if (fields == null)
                throw new ArgumentNullException("fields");

            Method = method;
            _fieldsByType = fields;
            ContentLength = GetIntegerByType(HeaderFieldType.ContentLength);
        }

        public IMethod Method { get; private set; }
        public int ContentLength { get; private set; }

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

        public static Header CreateFrom(IMethod method, IList<HeaderField> fields)
        {
            var fieldsByType = new Dictionary<HeaderFieldName, HeaderField>();
            var fieldsListByType = new Dictionary<HeaderFieldName, List<HeaderField>>();
            var fieldTypes = new HashSet<HeaderFieldName>();

            foreach (var field in fields)
                fieldTypes.Add(field.Name);

            foreach (var fieldType in fieldTypes)
                fieldsListByType[fieldType] = new List<HeaderField>();

            foreach (var field in fields)
                fieldsListByType[field.Name].Add(field);

            foreach (var fieldsList in fieldsListByType)
            {
                var type = fieldsList.Key;
                var currentFields = fieldsList.Value;

                if (!type.CanHaveMultipleValues && currentFields.Count > 1)
                    return null;

                var values = new List<string>();

                foreach (var currentField in currentFields)
                    values.AddRange(currentField.Values);

                fieldsByType[type] = new HeaderField(type, values);
            }

            return new Header(method, fieldsByType);
        }

        private int GetIntegerByType(HeaderFieldType type)
        {
            var field = this[new HeaderFieldName(type)];
            var value = field.Values[0];
            return int.Parse(value);
        }
    }
}
