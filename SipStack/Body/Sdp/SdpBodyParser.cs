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
            int currentIndex = 0;
            var protocolVersionResult = ParseMandatoryLine<VersionLine>(parsedLines[currentIndex]);

            if (protocolVersionResult.IsError)
                return protocolVersionResult.ToParseResult<IBody>();

            currentIndex++;
            var originatorLineResult = ParseMandatoryLine<OriginatorLine>(parsedLines[currentIndex]);

            if (originatorLineResult.IsError)
                return originatorLineResult.ToParseResult<IBody>();

            currentIndex++;
            var sessionNameResult = ParseMandatoryLine<SessionNameLine>(parsedLines[currentIndex]);

            if (sessionNameResult.IsError)
                return sessionNameResult.ToParseResult<IBody>();

            currentIndex++;
            var sessionDescription = ParseOptionalLine<DescriptionLine>(parsedLines[currentIndex]);

            if (sessionDescription != null)
                currentIndex++;

            var emailAddress = ParseOptionalLine<EmailAddressLine>(parsedLines[currentIndex]);

            if (emailAddress != null)
                currentIndex++;

            var phoneNumberLine = ParseOptionalLine<PhoneNumberLine>(parsedLines[currentIndex]);

            if (phoneNumberLine != null)
                currentIndex++;

            var connectionInformationLine = ParseOptionalLine<ConnectionInformationLine>(parsedLines[currentIndex]);

            if (connectionInformationLine != null)
                currentIndex++;

            var bandwidthLines = new List<BandwidthLine>();
            var previousIndex = currentIndex;

            while(true)
            {
                var bandwidthLine = ParseOptionalLine<BandwidthLine>(parsedLines[currentIndex]);

                if (bandwidthLine == null)
                    break;

                bandwidthLines.Add(bandwidthLine);
                currentIndex++;
            } 

            throw new NotImplementedException();
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

        private ParseResult<LineType> ParseMandatoryLine<LineType>(ILine currentLine) where LineType : class
        {
            var currentLineCasted = currentLine as LineType;
            if (currentLineCasted == null)
                return new ParseResult<LineType>($"line type {typeof(LineType).Name} is missing");

            return new ParseResult<LineType>(currentLineCasted);
        }

        private LineType ParseOptionalLine<LineType>(ILine currentLine) where LineType : class
        {
            var currentLineCasted = currentLine as LineType;
            return currentLineCasted;
        }
    }
}
