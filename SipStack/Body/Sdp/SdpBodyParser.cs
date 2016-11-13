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
            var originatorResult = ParseOriginatorLine(linesWithUsage);
            var uriResult = ParseUriLine(linesWithUsage);
            var sessionNameResult = ParseSessionNameLine(linesWithUsage);
            var sessionDescriptionResult = ParseSessionDescriptionLine(linesWithUsage);

            if (originatorResult.IsError)
                return originatorResult.ToParseResult<IBody>();

            var body = new SdpBody(originatorResult.Result, sessionNameResult.Result, sessionDescriptionResult.Result, uriResult.Result);       
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
            var linesWithType = lines.Where(x => x.Line is OriginatorLine).ToList();

            if (linesWithType.Count() > 1)
                return new ParseResult<OriginatorLine>($"there must be at most one originator line");

            if (linesWithType.Count() == 0)
                return new ParseResult<OriginatorLine>(null as OriginatorLine);

            var lineWithUsage = linesWithType[0];
            lineWithUsage.MarkAsUsed();
            return new ParseResult<OriginatorLine>(lineWithUsage.Line as OriginatorLine);
        }

        private ParseResult<UriLine> ParseUriLine(IReadOnlyList<LineWithUsage> lines)
        {
            var linesWithType = lines.Where(x => x.Line is UriLine).ToList();

            if (linesWithType.Count() > 1)
                return new ParseResult<UriLine>($"there must be at most one uri line");

            if (linesWithType.Count() == 0)
                return new ParseResult<UriLine>(null as UriLine);

            var lineWithUsage = linesWithType[0];
            lineWithUsage.MarkAsUsed();
            return new ParseResult<UriLine>(lineWithUsage.Line as UriLine);
        }

        private ParseResult<SessionNameLine> ParseSessionNameLine(IReadOnlyList<LineWithUsage> lines)
        {
            var linesWithType = lines.Where(x => x.Line is SessionNameLine).ToList();

            if (linesWithType.Count() != 1)
                return new ParseResult<SessionNameLine>($"there must be at exactly one session name line");

            var lineWithUsage = linesWithType[0];
            lineWithUsage.MarkAsUsed();
            return new ParseResult<SessionNameLine>(lineWithUsage.Line as SessionNameLine);
        }

        private ParseResult<DescriptionLine> ParseSessionDescriptionLine(IReadOnlyList<LineWithUsage> lines)
        {
            var linesWithType = lines.Where(x => x.Line is DescriptionLine).ToList();

            if (linesWithType.Count() != 1)
                return new ParseResult<DescriptionLine>($"there must be at most one general session name line");

            if (linesWithType.Count() == 0)
                return new ParseResult<DescriptionLine>(null as DescriptionLine);

            var lineWithUsage = linesWithType[0];
            lineWithUsage.MarkAsUsed();
            return new ParseResult<DescriptionLine>(lineWithUsage.Line as DescriptionLine);
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
