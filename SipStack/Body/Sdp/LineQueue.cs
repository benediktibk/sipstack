using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public class LineQueue
    {
        private readonly List<Tuple<char, string>> _lines;
        private int _currentIndex;

        public LineQueue(IReadOnlyList<Tuple<char, string>> lines)
        {
            _lines = lines.ToList();
            _currentIndex = 0;
        }

        public bool IsEmpty => _currentIndex >= _lines.Count();

        public ParseResult<LineType> ParseMandatoryLine<LineType>(char lineType, Func<string, ParseResult<LineType>> parser)
        {
            if (IsEmpty)
                return ParseResult<LineType>.CreateError($"line type {typeof(LineType).Name} is missing");

            var currentLineType = _lines[_currentIndex].Item1;
            var currentLineData = _lines[_currentIndex].Item2;

            if (currentLineType != lineType)
                return ParseResult<LineType>.CreateError($"the mandatory line {typeof(LineType).Name} is missing");

            _currentIndex++;
            return parser(currentLineData);
        }

        public ParseResult<LineType> ParseOptionalLine<LineType>(char lineType, Func<string, ParseResult<LineType>> parser)
        {
            if (IsEmpty)
                return ParseResult<LineType>.CreateEmptySuccess();

            var currentLineType = _lines[_currentIndex].Item1;
            var currentLineData = _lines[_currentIndex].Item2;

            if (currentLineType != lineType)
                return ParseResult<LineType>.CreateEmptySuccess();

            _currentIndex++;
            return parser(currentLineData);
        }

        public ParseResult<List<LineType>> ParseMultipleOptionalLines<LineType>(char lineType, Func<string, ParseResult<LineType>> parser)
        {
            var result = new List<LineType>();

            while (true)
            {
                var line = ParseOptionalLine(lineType, parser);

                if (line == null)
                    return ParseResult<List<LineType>>.CreateSuccess(result);

                if (line.IsError)
                    return line.ToParseResult<List<LineType>>();

                result.Add(line.Result);
            }
        }
    }
}
