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
            var originatorResult = ParseSessionGeneralLine<OriginatorLine>(linesWithUsage);
            var uriResult = ParseSessionGeneralLine<UriLine>(linesWithUsage);
            var sessionNameResult = ParseSessionGeneralLine<SessionNameLine>(linesWithUsage);
            var sessionDescriptionResult = ParseSessionGeneralLine<DescriptionLine>(linesWithUsage);

            if (originatorResult.IsError)
                return originatorResult.ToParseResult<IBody>();

            var body = new SdpBody(originatorResult.Result, sessionNameResult.Result, sessionDescriptionResult.Result, uriResult.Result);
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

        private ParseResult<LineType> ParseSessionGeneralLine<LineType>(IReadOnlyList<LineWithUsage> lines) 
            where LineType: class, ILine
        {
            var linesWithType = lines.Where(x => x.Line is LineType).ToList();

            if (linesWithType.Count() > 1)
                return new ParseResult<LineType>($"there must be at most one {typeof(LineType).Name}");

            if (linesWithType.Count() == 0)
                return new ParseResult<LineType>(null as LineType);

            var lineWithUsage = linesWithType[0];
            lineWithUsage.MarkAsUsed();
            return new ParseResult<LineType>(lineWithUsage.Line as LineType);
        }
    }
}
