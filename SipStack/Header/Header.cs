using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Header
{
    public class Header : IHeader
    {
        #region variables

        private static IList<HeaderFieldType> _fieldsAtStart;
        private static IList<HeaderFieldType> _fieldsAtEnd;
        private static HashSet<HeaderFieldType> _allFieldsAtEndOrStart;
        private IDictionary<HeaderFieldName, HeaderField> _fieldsByType;
        private int _contentLength;
        private string _contentType;

        #endregion

        #region constructors

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
        }

        #endregion

        #region properties

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

        public IMethod Method { get; private set; }

        public int ContentLength
        {
            get
            {
                if (_contentLength < 0)
                {
                    _contentLength = GetIntegerByType(HeaderFieldType.ContentLength);

                    if (_contentLength < 0)
                        throw new Exception("negative value for content length is invalid");
                }

                return _contentLength;
            }
        }

        public string ContentType
        {
            get
            {
                if (_contentType == null)
                    _contentType = GetStringByType(HeaderFieldType.ContentType);

                return _contentType;
            }
        }

        #endregion

        #region public functions

        public static ParseResult<Header> CreateFrom(IMethod method, IList<HeaderField> fields)
        {
            var fieldsByType = new Dictionary<HeaderFieldName, HeaderField>();
            var fieldsListByType = new Dictionary<HeaderFieldName, List<HeaderField>>();
            var fieldTypes = new HashSet<HeaderFieldName>();

            foreach (var field in fields)
                fieldTypes.Add(field.Name);

            if (!fieldTypes.Contains(new HeaderFieldName(HeaderFieldType.ContentLength)))
                return new ParseResult<Header>("required field Content-Length is missing");

            foreach (var fieldType in fieldTypes)
                fieldsListByType[fieldType] = new List<HeaderField>();

            foreach (var field in fields)
                fieldsListByType[field.Name].Add(field);

            foreach (var fieldsList in fieldsListByType)
            {
                var type = fieldsList.Key;
                var currentFields = fieldsList.Value;

                if (!type.CanHaveMultipleValues && currentFields.Count > 1)
                    return new ParseResult<Header>("a header field has multiple values, although it is not allowed to have multiple values");

                var values = new List<string>();

                foreach (var currentField in currentFields)
                    values.AddRange(currentField.Values);

                fieldsByType[type] = new HeaderField(type, values);
            }

            return new ParseResult<Header>(new Header(method, fieldsByType));
        }

        public void AddTo(MessageBuilder messageBuilder)
        {
            Method.AddTo(messageBuilder);

            foreach (var type in _fieldsAtStart)
            {
                HeaderField field;

                if (_fieldsByType.TryGetValue(new HeaderFieldName(type), out field))
                    field.AddTo(messageBuilder);
            }

            foreach (var field in _fieldsByType.Select(x => x.Value))
            {
                if (field.Name.IsContainedIn(_allFieldsAtEndOrStart))
                    continue;

                field.AddTo(messageBuilder);
            }

            messageBuilder.AddLineFormat("{0}: {1}", HeaderFieldType.ContentLength.ToFriendlyString(), ContentLength.ToString());
        }

        #endregion

        #region private functions

        private int GetIntegerByType(HeaderFieldType type)
        {
            var field = this[new HeaderFieldName(type)];
            var value = field.Values[0];
            return int.Parse(value);
        }

        private string GetStringByType(HeaderFieldType type)
        {
            var field = this[new HeaderFieldName(type)];
            var value = field.Values[0];
            return value;
        }

        #endregion
    }
}
