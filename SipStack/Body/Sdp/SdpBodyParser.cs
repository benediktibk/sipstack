using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public class SdpBodyParser : IBodyParser
    {
        private readonly LineParser _lineParser;

        public SdpBodyParser(LineParser lineParser)
        {
            _lineParser = lineParser;
        }

        public ParseResult<IBody> Parse(IList<string> lines, int startLine, int endLine)
        {
            var parsedLinesResult = ParseLines(lines, startLine, endLine);

            if (parsedLinesResult.IsError)
                return parsedLinesResult.ToParseResult<IBody>();

            var parsedLines = parsedLinesResult.Result;
            var linesWithUsage = parsedLines.Select(x => new LineWithUsage(x)).ToList();
            var originatorLineResult = ParseOriginatorLine(linesWithUsage);

            if (originatorLineResult.IsError)
                return originatorLineResult.ToParseResult<IBody>();

            var body = new SdpBody(originatorLineResult.Result);
            throw new NotImplementedException();        
            return new ParseResult<IBody>(body);
        }

        private ParseResult<List<ILine>> ParseLines(IList<string> lines, int startLine, int endLine)
        {
            var parsedLines = new List<ILine>();
            parsedLines.Capacity = endLine - startLine + 1;

            for (var i = startLine; i <= endLine; ++i)
            {
                var line = lines[i];
                var parsedLine = _lineParser.Parse(line);

                if (parsedLine.IsError)
                    return parsedLine.ToParseResult<List<ILine>>();

                parsedLines.Add(parsedLine.Result);
            }

            return new ParseResult<List<ILine>>(parsedLines);
        }

        private ParseResult<OriginatorLine> ParseOriginatorLine(IReadOnlyList<LineWithUsage> lines)
        {
            var originatorLines = lines.Where(x => x.Line is OriginatorLine).ToList();

            if (originatorLines.Count() != 1)
                return new ParseResult<OriginatorLine>("there must be exactly one originator line");

            var originatorLineWithUsage = originatorLines[0];
            originatorLineWithUsage.MarkAsUsed();
            return new ParseResult<OriginatorLine>(originatorLineWithUsage.Line as OriginatorLine);
        }
    }
}
