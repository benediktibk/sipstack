using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            foreach(var line in _lines)
            {
                capacity += line.Length + 2;
            }

            _stringBuilder.Clear();
            _stringBuilder.EnsureCapacity(capacity);

            foreach (var line in _lines)
                _stringBuilder.AppendFormat("{0}\r\n", line);

            var result = _stringBuilder.ToString();
            Debug.Assert(result.Length == capacity);
            return result;
        }
    }
}
