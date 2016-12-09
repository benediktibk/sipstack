using SipStack.Utils;
using System;
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
            var requestLineResult = _requestLineParser.Parse(lines[0]);
            if (requestLineResult.IsError)
                return requestLineResult.ToParseResult<Header>();

            var fieldsResult = ParseHeaderFields(lines, headerEnd);

            if (fieldsResult.IsError)
                return fieldsResult.ToParseResult<Header>();

            var fields = fieldsResult.Result;
            var fieldsCombined = CombineHeaderFieldsWithSameType(fields);

            var header = new HeaderDto();

            throw new NotImplementedException();

            return new ParseResult<Header>(new Header(header));
        }

        private ParseResult<List<HeaderField>> ParseHeaderFields(IReadOnlyList<string> lines, int headerEnd)
        {
            var fields = new List<HeaderField>();
            for (var i = 1; i < headerEnd; ++i)
            {
                var end = 0;
                var headerFieldResult = _headerFieldParser.Parse(lines, i, out end);

                if (headerFieldResult.IsError)
                    return headerFieldResult.ToParseResult<List<HeaderField>>();

                var headerField = headerFieldResult.Result;
                fields.Add(headerField);

                i = end;
            }

            return new ParseResult<List<HeaderField>>(fields);
        }

        private static List<HeaderField> CombineHeaderFieldsWithSameType(List<HeaderField> fields)
        {
            var fieldsByType = new Dictionary<HeaderFieldName, List<string>>();

            foreach (var field in fields)
            {
                var type = field.Name;
                List<string> values;

                if (!fieldsByType.TryGetValue(type, out values))
                {
                    values = new List<string>(1);
                    fieldsByType.Add(type, values);
                }


                values.AddRange(field.Values);
            }

            return fieldsByType.Select(x => new HeaderField(x.Key, x.Value)).ToList();
        }
    }
}
