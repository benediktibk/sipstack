using System;
using System.Collections.Generic;

namespace SipStack.Body
{
    public class SdpBodyParser : IBodyParser
    {
        private readonly SdpLineParser _lineParser;

        public SdpBodyParser(SdpLineParser lineParser)
        {
            _lineParser = lineParser;
        }

        public ParseResult<IBody> Parse(IList<string> lines, int startLine, int endLine)
        {
            var parsedLines = new List<ISdpLine>();
            parsedLines.Capacity = endLine - startLine + 1;

            for (var i = startLine; i <= endLine; ++i)
            {
                var line = lines[i];
                var parsedLine = _lineParser.Parse(line);

                if (parsedLine.IsError)
                    return parsedLine.ToParseResult<IBody>();

                parsedLines.Add(parsedLine.Result);
            }

            var body = SdpBody.CreateFrom(parsedLines);

            if (body.IsError)
                return body.ToParseResult<IBody>();

            return new ParseResult<IBody>(body.Result);
        }
    }
}
