﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SipStack.Header
{
    public class HeaderFieldParser
    {
        public ParseResult<HeaderField> Parse(IList<string> lines, int start, out int end)
        {
            var startLine = lines[start];
            end = start;

            if (string.IsNullOrEmpty(startLine))
                return new ParseResult<HeaderField>($"empty header line");

            var pattern = @"([^  \t]*)[ \t]*:[ \t]*(.*)";
            var matches = Regex.Matches(lines[start], pattern);

            if (matches.Count != 1)
                return new ParseResult<HeaderField>($"the header line '{lines[start]}' is malformed");

            var fieldName = new HeaderFieldName(matches[0].Groups[1].Value);
            var stringBuilder = new StringBuilder(matches[0].Groups[2].Value);
            pattern = @"^(\.|[ \t]+)([^ \t].*)$|^[ \t]+$";

            while (end + 1 < lines.Count)
            {
                matches = Regex.Matches(lines[end + 1], pattern);

                if (matches.Count == 0)
                    break;

                end = end + 1;

                if (matches[0].Groups.Count > 1)
                    stringBuilder.AppendFormat(" {0}", matches[0].Groups[2].Value);
            }       

            var fieldValues = stringBuilder.ToString();
            var fieldValuesAsList = SeparateFieldValues(fieldName, fieldValues);

            return new ParseResult<HeaderField>(new HeaderField(fieldName, fieldValuesAsList));
        }

        private static IList<string> SeparateFieldValues(HeaderFieldName fieldName, string fieldValues)
        {
            var result = new List<string>();

            if (!fieldName.CanHaveMultipleValues)
            {
                result.Add(fieldValues);
                return result;
            }
            
            var pattern = @"([^ ,][^,]*[^ ,])(([ ]*,)|$)";
            var matches = Regex.Matches(fieldValues, pattern);
            result.Capacity = matches.Count;

            foreach (Match match in matches)
                result.Add(match.Groups[1].Value);
            
            return result;
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

        private static int IndexOfNoneWhitespaceBackwards(string line, int start)
        {
            for (var i = start; i >= 0; --i)
            {
                var current = line[i];

                if (current != ' ' && current != '\t')
                    return i;
            }

            return -1;
        }
    }
}
