using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class BodyParser : IBodyParser
    {
        private readonly LineParser _lineParser;

        public BodyParser(LineParser lineParser)
        {
            _lineParser = lineParser;
        }

        public ParseResult<IBody> Parse(IList<string> lines, int startLine, int endLine)
        {
            if (endLine < startLine)
                throw new ArgumentException("startLine must be less or equal than endLine");

            if (startLine < 0 || endLine < 0)
                throw new ArgumentException("startLine and endLine must not be negative");

            if (lines.Count() <= endLine)
                throw new ArgumentException("endLine is greater than the element count in lines");

            var lineTypes = ParseLineTypes(lines, startLine, endLine);

            if (lineTypes.IsError)
                return lineTypes.ToParseResult<IBody>();

            var lineQueue = new LineQueue(lineTypes.Result);
            var protocolVersionResult = lineQueue.ParseMandatoryLine<Version>();
            var originatorLineResult = lineQueue.ParseMandatoryLine<OriginatorLine>();
            var sessionNameResult = lineQueue.ParseMandatoryLine<SessionNameLine>();

            if (protocolVersionResult.IsError)
                return protocolVersionResult.ToParseResult<IBody>();            

            if (originatorLineResult.IsError)
                return originatorLineResult.ToParseResult<IBody>();            

            if (sessionNameResult.IsError)
                return sessionNameResult.ToParseResult<IBody>();
            
            var sessionDescription = lineQueue.ParseOptionalLine<Description>();
            var uri = lineQueue.ParseOptionalLine<HttpUri>();
            var emailAddress = lineQueue.ParseOptionalLine<EmailAddressLine>();
            var phoneNumberLine = lineQueue.ParseOptionalLine<PhoneNumberLine>();
            var connectionInformationLines = lineQueue.ParseMultipleOptionalLines<ConnectionInformationLine>();
            var bandwidthLines = lineQueue.ParseMultipleOptionalLines<BandwidthLine>();
            var timeDescriptions = ParseTimeDescriptions(lineQueue);
            var timeZoneLine = lineQueue.ParseOptionalLine<TimeZoneLine>();
            var encryptionKey = lineQueue.ParseOptionalLine<EncryptionKeyLine>();
            var sessionAttributes = lineQueue.ParseMultipleOptionalLines<AttributeLine>();
            var mediaDescriptions = ParseMediaDescriptions(lineQueue);

            if (!lineQueue.IsEmpty)
                return new ParseResult<IBody>("there are invalid lines in the SDP-Body");

            var sdpBody = new Body(
                protocolVersionResult.Result.Version,
                originatorLineResult.Result.Originator,
                sessionNameResult.Result.Name,
                sessionDescription?.Description,
                uri?.Uri,
                emailAddress?.EmailAddress,
                phoneNumberLine?.PhoneNumber,
                connectionInformationLines.Select(x => x.ConnectionInformation),
                bandwidthLines.Select(x => x.Bandwidth),
                timeDescriptions,
                timeZoneLine == null ? new List<TimeZoneAdjustment>() : timeZoneLine.TimeZoneAdjustments,
                encryptionKey?.EncryptionKey,
                sessionAttributes.Select(x => x.Attribute),
                mediaDescriptions);

            return new ParseResult<IBody>(sdpBody);
        }

        private static ParseResult<List<Tuple<char, string>>> ParseLineTypes(IList<string> lines, int startLine, int endLine)
        {
            var parsedLines = new List<Tuple<char, string>>();
            parsedLines.Capacity = endLine - startLine + 1;

            for (var i = startLine; i <= endLine; ++i)
            {
                var line = lines[i];
                var parsedLine = ParseLineType(line);

                if (parsedLine.IsError)
                    return parsedLine.ToParseResult<List<Tuple<char, string>>>();

                parsedLines.Add(parsedLine.Result);
            }

            return new ParseResult<List<Tuple<char, string>>>(parsedLines);
        }

        private static ParseResult<Tuple<char, string>> ParseLineType(string line)
        {
            var pattern = @"^([a-z])=(.*)$";
            var matches = Regex.Matches(line, pattern);

            if (matches.Count != 1)
                return new ParseResult<Tuple<char, string>>($"the line '{line}' is malformed");

            var match = matches[0];
            var type = match.Groups[1].Value;
            var data = match.Groups[2].Value;

            if (type.Length != 1)
                return new ParseResult<Tuple<char, string>>($"the line '{line}' is malformed");

            return new ParseResult<Tuple<char, string>>(new Tuple<char, string>(type.First(), data));
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
                result.Add(new TimeDescription(currentLineParsed.Timing, repeatings.Select(x => x.Repeat)));
            }
        }

        private List<MediaDescription> ParseMediaDescriptions(LineQueue lineQueue)
        {
            var result = new List<MediaDescription>();

            while (true)
            {
                var mediaLine = lineQueue.ParseOptionalLine<MediaLine>();

                if (mediaLine == null)
                    return result;

                var mediaTitle = lineQueue.ParseOptionalLine<Description>();
                var connectionInformation = lineQueue.ParseMultipleOptionalLines<ConnectionInformationLine>();
                var bandwidths = lineQueue.ParseMultipleOptionalLines<BandwidthLine>();
                var encryptionKey = lineQueue.ParseOptionalLine<EncryptionKeyLine>();
                var attributes = lineQueue.ParseMultipleOptionalLines<AttributeLine>();

                var mediaDescription = new MediaDescription(
                    mediaLine.Media,
                    mediaTitle?.Description,
                    connectionInformation.Select(x => x.ConnectionInformation),
                    bandwidths.Select(x => x.Bandwidth),
                    encryptionKey?.EncryptionKey,
                    attributes.Select(x => x.Attribute));

                result.Add(mediaDescription);
            }
        }
    }
}
