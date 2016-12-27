using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class BodyParser : IBodyParser
    {
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
            var protocolVersionResult = lineQueue.ParseMandatoryLine('v', LineParsers.Version);
            var originatorLineResult = lineQueue.ParseMandatoryLine('o', LineParsers.Originator);
            var sessionNameResult = lineQueue.ParseMandatoryLine('s', LineParsers.SessionName);            
            var sessionDescription = lineQueue.ParseOptionalLine('i', LineParsers.Description);
            var uri = lineQueue.ParseOptionalLine('u', LineParsers.HttpUri);
            var emailAddress = lineQueue.ParseOptionalLine('e', LineParsers.EmailAddress);
            var phoneNumberLine = lineQueue.ParseOptionalLine('u', LineParsers.PhoneNumber);
            var connectionInformationLines = lineQueue.ParseMultipleOptionalLines('c', LineParsers.ConnectionInformation);
            var bandwidthLines = lineQueue.ParseMultipleOptionalLines('b', LineParsers.Bandwidth);
            var timeDescriptions = ParseTimeDescriptions(lineQueue);
            var timeZoneLine = lineQueue.ParseOptionalLine('z', LineParsers.TimeZoneAdjustment);
            var encryptionKey = lineQueue.ParseOptionalLine('k', LineParsers.EncryptionKey);
            var sessionAttributes = lineQueue.ParseMultipleOptionalLines('a', LineParsers.Attribute);
            var mediaDescriptions = ParseMediaDescriptions(lineQueue);

            if (!lineQueue.IsEmpty)
                return ParseResult<IBody>.CreateError("there are invalid lines in the SDP-Body");

            if (protocolVersionResult.IsError)
                return protocolVersionResult.ToParseResult<IBody>();

            if (originatorLineResult.IsError)
                return originatorLineResult.ToParseResult<IBody>();

            if (sessionNameResult.IsError)
                return sessionNameResult.ToParseResult<IBody>();

            if (sessionDescription.IsError)
                return sessionDescription.ToParseResult<IBody>();

            if (uri.IsError)
                return uri.ToParseResult<IBody>();

            if (emailAddress.IsError)
                return emailAddress.ToParseResult<IBody>();

            if (phoneNumberLine.IsError)
                return phoneNumberLine.ToParseResult<IBody>();

            if (connectionInformationLines.IsError)
                return connectionInformationLines.ToParseResult<IBody>();

            if (bandwidthLines.IsError)
                return bandwidthLines.ToParseResult<IBody>();

            if (timeDescriptions.IsError)
                return timeDescriptions.ToParseResult<IBody>();

            if (timeZoneLine.IsError)
                return timeZoneLine.ToParseResult<IBody>();

            if (encryptionKey.IsError)
                return encryptionKey.ToParseResult<IBody>();

            if (sessionAttributes.IsError)
                return sessionAttributes.ToParseResult<IBody>();

            if (mediaDescriptions.IsError)
                return mediaDescriptions.ToParseResult<IBody>();

            var sdpBody = new Body(
                protocolVersionResult.Result.Value,
                originatorLineResult.Result,
                sessionNameResult.Result,
                sessionDescription.Result,
                uri.Result?.Uri,
                emailAddress.Result,
                phoneNumberLine.Result,
                connectionInformationLines.Result,
                bandwidthLines.Result,
                timeDescriptions.Result,
                timeZoneLine.Result,
                encryptionKey.Result,
                sessionAttributes.Result,
                mediaDescriptions.Result);

            return ParseResult<IBody>.CreateSuccess(sdpBody);
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

            return ParseResult<List<Tuple<char, string>>>.CreateSuccess(parsedLines);
        }

        private static ParseResult<Tuple<char, string>> ParseLineType(string line)
        {
            var pattern = @"^([a-z])=(.*)$";
            var matches = Regex.Matches(line, pattern);

            if (matches.Count != 1)
                return ParseResult<Tuple<char, string>>.CreateError($"the line '{line}' is malformed");

            var match = matches[0];
            var type = match.Groups[1].Value;
            var data = match.Groups[2].Value;

            if (type.Length != 1)
                return ParseResult<Tuple<char, string>>.CreateError($"the line '{line}' is malformed");

            return ParseResult<Tuple<char, string>>.CreateSuccess(new Tuple<char, string>(type.First(), data));
        }

        private static ParseResult<List<TimeDescription>> ParseTimeDescriptions(LineQueue lineQueue)
        {
            var result = new List<TimeDescription>();

            while(true)
            {
                var timing = lineQueue.ParseOptionalLine('t', LineParsers.Timing);

                if (timing.IsError)
                    return timing.ToParseResult<List<TimeDescription>>();

                if (timing.Result == null)
                    return ParseResult<List<TimeDescription>>.CreateSuccess(result);

                var repeatings = lineQueue.ParseMultipleOptionalLines('r', LineParsers.Repeat);

                if (repeatings.IsError)
                    return timing.ToParseResult<List<TimeDescription>>();

                result.Add(new TimeDescription(timing.Result, repeatings.Result));
            }
        }

        private static ParseResult<List<MediaDescription>> ParseMediaDescriptions(LineQueue lineQueue)
        {
            var result = new List<MediaDescription>();

            while (true)
            {
                var media = lineQueue.ParseOptionalLine('m', LineParsers.Media);

                if (media.Result == null)
                    return ParseResult<List<MediaDescription>>.CreateSuccess(result);

                var mediaTitle = lineQueue.ParseOptionalLine('i', LineParsers.Description);
                var connectionInformation = lineQueue.ParseMultipleOptionalLines('c', LineParsers.ConnectionInformation);
                var bandwidths = lineQueue.ParseMultipleOptionalLines('b', LineParsers.Bandwidth);
                var encryptionKey = lineQueue.ParseOptionalLine('k', LineParsers.EncryptionKey);
                var attributes = lineQueue.ParseMultipleOptionalLines('a', LineParsers.Attribute);

                if (media.IsError)
                    return media.ToParseResult<List<MediaDescription>>();

                if (connectionInformation.IsError)
                    return connectionInformation.ToParseResult<List<MediaDescription>>();

                if (bandwidths.IsError)
                    return bandwidths.ToParseResult<List<MediaDescription>>();

                if (encryptionKey.IsError)
                    return encryptionKey.ToParseResult<List<MediaDescription>>();

                if (attributes.IsError)
                    return attributes.ToParseResult<List<MediaDescription>>();

                var mediaDescription = new MediaDescription(
                    media.Result,
                    mediaTitle.Result,
                    connectionInformation.Result,
                    bandwidths.Result,
                    encryptionKey.Result,
                    attributes.Result);

                result.Add(mediaDescription);
            }
        }
    }
}
