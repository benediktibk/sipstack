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

        public void AddLineFormat(string format, string paramOne)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat(format, paramOne);
            _lines.Add(_stringBuilder.ToString());
        }

        public void AddLineFormat(string format, string paramOne, string paramTwo)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat(format, paramOne, paramTwo);
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
