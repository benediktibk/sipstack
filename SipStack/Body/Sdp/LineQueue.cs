using SipStack.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public class LineQueue
    {
        private readonly List<ILine> _lines;
        private int _currentIndex;

        public LineQueue(IReadOnlyList<ILine> lines)
        {
            _lines = lines.ToList();
            _currentIndex = 0;
        }

        public bool IsEmpty => _currentIndex >= _lines.Count();

        public ParseResult<LineType> ParseMandatoryLine<LineType>() where LineType : class
        {
            if (IsEmpty)
                return new ParseResult<LineType>($"line type {typeof(LineType).Name} is missing");

            var currentLineCasted = _lines[_currentIndex] as LineType;
            if (currentLineCasted == null)
                return new ParseResult<LineType>($"line type {typeof(LineType).Name} is missing");

            _currentIndex++;
            return new ParseResult<LineType>(currentLineCasted);
        }

        public LineType ParseOptionalLine<LineType>() where LineType : class
        {
            if (IsEmpty)
                return null;

            var currentLineCasted = _lines[_currentIndex] as LineType;

            if (currentLineCasted != null)
                _currentIndex++;

            return currentLineCasted;
        }
    }
}
