﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SipStack
{
    public class Header : IHeader
    {
        private static IList<HeaderFieldType> _fieldsAtStart;
        private static IList<HeaderFieldType> _fieldsAtEnd;
        private static HashSet<HeaderFieldType> _allFieldsAtEndOrStart;
        private IDictionary<HeaderFieldName, HeaderField> _fieldsByType;

        static Header()
        {
            _fieldsAtStart = new List<HeaderFieldType>
            {
                HeaderFieldType.From,
                HeaderFieldType.To
            };

            _fieldsAtEnd = new List<HeaderFieldType>
            {
                HeaderFieldType.ContentLength,
                HeaderFieldType.ContentType
            };

            _allFieldsAtEndOrStart = new HashSet<HeaderFieldType>();

            foreach (var field in _fieldsAtStart)
                _allFieldsAtEndOrStart.Add(field);
            foreach (var field in _fieldsAtEnd)
                _allFieldsAtEndOrStart.Add(field);
        }

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

        public static ParseResult<Header> CreateFrom(IMethod method, IList<HeaderField> fields)
        {
            var fieldsByType = new Dictionary<HeaderFieldName, HeaderField>();
            var fieldsListByType = new Dictionary<HeaderFieldName, List<HeaderField>>();
            var fieldTypes = new HashSet<HeaderFieldName>();

            foreach (var field in fields)
                fieldTypes.Add(field.Name);

            if (!fieldTypes.Contains(new HeaderFieldName(HeaderFieldType.ContentLength)))
                return new ParseResult<Header>(ParseError.ContentLengthLineMissing, "required field Content-Length is missing");

            foreach (var fieldType in fieldTypes)
                fieldsListByType[fieldType] = new List<HeaderField>();

            foreach (var field in fields)
                fieldsListByType[field.Name].Add(field);

            foreach (var fieldsList in fieldsListByType)
            {
                var type = fieldsList.Key;
                var currentFields = fieldsList.Value;

                if (!type.CanHaveMultipleValues && currentFields.Count > 1)
                    return new ParseResult<Header>(ParseError.HeaderFieldWithForbiddenMultipleValues, "a header field has multiple values, although it is not allowed to have multiple values");

                var values = new List<string>();

                foreach (var currentField in currentFields)
                    values.AddRange(currentField.Values);

                fieldsByType[type] = new HeaderField(type, values);
            }

            return new ParseResult<Header>(new Header(method, fieldsByType));
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("{0}\r\n", Method.ToString());

            foreach (var type in _fieldsAtStart)
            {
                HeaderField field;

                if (_fieldsByType.TryGetValue(new HeaderFieldName(type), out field))
                    stringBuilder.AppendFormat("{0}\r\n", field.ToString());
            }

            foreach (var field in _fieldsByType.Select(x => x.Value))
            {
                if (field.Name.IsContainedIn(_allFieldsAtEndOrStart))
                    continue;
                
                stringBuilder.AppendFormat("{0}\r\n", field.ToString());
            }

            stringBuilder.AppendFormat("{0}: {1}\r\n", HeaderFieldType.ContentLength.ToFriendlyString(), ContentLength);
            stringBuilder.Append("\r\n");

            return stringBuilder.ToString();
        }

        private int GetIntegerByType(HeaderFieldType type)
        {
            var field = this[new HeaderFieldName(type)];
            var value = field.Values[0];
            return int.Parse(value);
        }
    }
}
