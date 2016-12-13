using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SipStack
{
    public class MessageBuilder
    {
        private IList<string> _lines;
        private StringBuilder _stringBuilder;

        public MessageBuilder()
        {
            _lines = new List<string>();
            _stringBuilder = new StringBuilder();
        }

        public void AddLine(string line)
        {
            _lines.Add(line);
        }

        public void AddLineFormat(string format, params string[] paramList)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat(format, paramList);
            _lines.Add(_stringBuilder.ToString());
        }

        public void AddSipHeaderLineWithMultipleValues(string headerName, IReadOnlyList<string> values)
        {
            if (values.Count < 1)
                throw new ArgumentException("values", "must contain at least one value");

            _stringBuilder.Clear();
            _stringBuilder.Append(headerName);
            _stringBuilder.Append(": ");

            for (var i = 0; i < values.Count - 1; ++i)
            {
                _stringBuilder.Append(values[i]);
                _stringBuilder.Append(", ");
            }

            _stringBuilder.Append(values[values.Count - 1]);
            _lines.Add(_stringBuilder.ToString());
        }

        public override string ToString()
        {
            var capacity = 0;

            for (var i = 0; i < _lines.Count - 1; ++i)
                capacity += _lines[i].Length + 2;

            if (_lines.Count > 0)
                capacity += _lines.Last().Length;

            _stringBuilder.Clear();
            _stringBuilder.EnsureCapacity(capacity);

            for(var i = 0; i < _lines.Count - 1; ++i)
                _stringBuilder.AppendFormat("{0}\r\n", _lines[i]);

            if (_lines.Count > 0)
                _stringBuilder.AppendFormat("{0}", _lines.Last());

            var result = _stringBuilder.ToString();
            Debug.Assert(result.Length == capacity);
            return result;
        }
    }
}
