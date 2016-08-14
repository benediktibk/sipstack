﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SipStack
{
    public class HeaderFieldParser
    {
        public ParseResult<HeaderField> Parse(IList<string> lines, int start)
        {
            var startLine = lines[start];

            if (string.IsNullOrEmpty(startLine))
                return new ParseResult<HeaderField>(ParseError.InvalidHeaderField, $"empty header line");

            var indexOfDelimiter = startLine.IndexOf(':');
            var indexOfWhitespace = startLine.IndexOf(' ');

            if (indexOfDelimiter < 0)
                return new ParseResult<HeaderField>(ParseError.InvalidHeaderField, $"missing colon in header field: {startLine}");

            var nameStart = 0;
            var nameEnd = 0;
            var valueStart = 0;
            var valueEnd = 0;

            // no whitespaces at all (Subject:lunch)
            if (indexOfWhitespace < 0)
            {
                nameStart = 0;
                nameEnd = indexOfDelimiter - 1;
                valueStart = indexOfDelimiter + 1;
                valueEnd = startLine.Length - 1;
            }
            // whitespace after colon (Subject: lunch)
            else if (indexOfWhitespace > indexOfDelimiter)
            {
                nameStart = 0;
                nameEnd = indexOfDelimiter - 1;
                var indexOfNoneWhitespace = IndexOfNoneWhitespace(startLine, indexOfWhitespace);

                if (indexOfNoneWhitespace < 0)
                    indexOfNoneWhitespace = startLine.Length;

                valueStart = indexOfNoneWhitespace;
                valueEnd = startLine.Length - 1;
            }
            // whitespace before colon (Subject    :  lunch)
            else
            {
                nameStart = 0;
                nameEnd = indexOfWhitespace - 1;
                var indexOfNoneWhitespace = IndexOfNoneWhitespace(startLine, indexOfDelimiter + 1);

                if (indexOfNoneWhitespace < 0)
                    indexOfNoneWhitespace = startLine.Length;

                valueStart = indexOfNoneWhitespace;
                valueEnd = startLine.Length - 1;
            }

            var additionalLines = CountNextLinesWithWhitespaceInFront(lines, start + 1);
            var stringBuilder = new StringBuilder(startLine.Substring(valueStart, valueEnd - valueStart + 1));

            for (var i = 0; i < additionalLines; ++i)
            {
                var currentLine = lines[start + 1 + i];
                var indexOfNoneWhitespace = IndexOfNoneWhitespace(currentLine, 1);
                var additionalLine = indexOfNoneWhitespace >= 0 ? currentLine.Substring(indexOfNoneWhitespace) : "";
                stringBuilder.AppendFormat(" {0}", additionalLine);
            }            

            var fieldName = startLine.Substring(nameStart, nameEnd - nameStart + 1);
            var fieldValue = stringBuilder.ToString();

            return new ParseResult<HeaderField>(new HeaderField { Name = fieldName, Value = fieldValue });
        }

        private static int CountNextLinesWithWhitespaceInFront(IList<string> lines, int start)
        {
            for(var i = start; i < lines.Count; ++i)
            {
                var firstCharacter = lines[i][0];

                if (firstCharacter != ' ' && firstCharacter != '\t')
                    return i - start;
            }

            return lines.Count - start;
        }

        private static int IndexOfNoneWhitespace(string line, int start)
        {
            for (var i = start; i < line.Length; ++i)
            {
                var current = line[i];

                if (current != ' ' && current != '\t')
                    return i;
            }

            return -1;
        }
    }
}
