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

            var lineQueue = new LineQueue(parsedLinesResult.Result);
            var protocolVersionResult = lineQueue.ParseMandatoryLine<VersionLine>();
            var originatorLineResult = lineQueue.ParseMandatoryLine<OriginatorLine>();
            var sessionNameResult = lineQueue.ParseMandatoryLine<SessionNameLine>();

            if (protocolVersionResult.IsError)
                return protocolVersionResult.ToParseResult<IBody>();            

            if (originatorLineResult.IsError)
                return originatorLineResult.ToParseResult<IBody>();            

            if (sessionNameResult.IsError)
                return sessionNameResult.ToParseResult<IBody>();
            
            var sessionDescription = lineQueue.ParseOptionalLine<DescriptionLine>();
            var emailAddress = lineQueue.ParseOptionalLine<EmailAddressLine>();
            var phoneNumberLine = lineQueue.ParseOptionalLine<PhoneNumberLine>();
            var connectionInformationLine = lineQueue.ParseOptionalLine<ConnectionInformationLine>();
            var bandwidthLines = lineQueue.ParseMultipleOptionalLines<BandwidthLine>();
            var timeDescriptions = ParseTimeDescriptions(lineQueue);
            var timeZoneAdjustment = lineQueue.ParseOptionalLine<TimeZoneAdjustment>();
            var encryptionKey = lineQueue.ParseOptionalLine<EncryptionKeyLine>();
            var sessionAttributes = lineQueue.ParseMultipleOptionalLines<AttributeLine>();

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

        private List<TimeDescription> ParseTimeDescriptions(LineQueue lineQueue)
        {
            var result = new List<TimeDescription>();

            while(true)
            {
                var currentLineParsed = lineQueue.ParseOptionalLine<TimeLine>();

                if (currentLineParsed == null)
                    return result;

                var repeatings = lineQueue.ParseMultipleOptionalLines<RepeatLine>();
                result.Add(new TimeDescription(currentLineParsed, repeatings));
            }
        }
    }
}
