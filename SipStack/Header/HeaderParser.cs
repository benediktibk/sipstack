using SipStack.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Header
{
    public class HeaderParser
    {
        private readonly RequestLineParser _requestLineParser;
        private readonly HeaderFieldParser _headerFieldParser;

        public HeaderParser(RequestLineParser requestLineParser, HeaderFieldParser headerFieldParser)
        {
            _requestLineParser = requestLineParser;
            _headerFieldParser = headerFieldParser;
        }

        public ParseResult<Header> Parse(IReadOnlyList<string> lines, int headerEnd)
        {
            var fields = new List<HeaderField>();

            var requestLineResult = _requestLineParser.Parse(lines[0]);
            if (requestLineResult.IsError)
                return requestLineResult.ToParseResult<Header>();

            for (var i = 1; i < headerEnd; ++i)
            {
                var end = 0;
                var headerFieldResult = _headerFieldParser.Parse(lines, i, out end);

                if (headerFieldResult.IsError)
                    return headerFieldResult.ToParseResult<Header>();

                var headerField = headerFieldResult.Result;
                fields.Add(headerField);

                i = end;
            }

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

            var header = new HeaderDto();

            return new ParseResult<Header>(new Header(header));
        }
    }
}
