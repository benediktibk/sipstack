using System;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class LineParser
    {
        public ParseResult<ILine> Parse(string line)
        {
            var pattern = @"^([a-z])=(.*)$";
            var matches = Regex.Matches(line, pattern);

            if (matches.Count != 1)
                return new ParseResult<ILine>($"the line '{line}' is malformed");

            var match = matches[0];
            var type = match.Groups[1].Value;
            var data = match.Groups[2].Value;
            return CreateFrom(type[0], data);
        }

        private ParseResult<ILine> CreateFrom(char type, string data)
        {
            switch (type)
            {
                case 'v': return VersionLine.CreateFrom(data);
                case 'o': return OriginatorLine.CreateFrom(data);
                case 's': return SessionNameLine.CreateFrom(data);
                case 'i': return DescriptionLine.CreateFrom(data);
                case 'u': return UriLine.CreateFrom(data);
                case 'e': return EmailAddressLine.CreateFrom(data);
                case 'p': return PhoneNumberLine.CreateFrom(data);
                case 'c': return ConnectionInformationLine.CreateFrom(data);
                case 'b': return BandwidthLine.CreateFrom(data);
                case 'z': return TimeZoneLine.CreateFrom(data);
                case 'k': return EncryptionKeyLine.CreateFrom(data);
                case 'a': return AttributeLine.CreateFrom(data);
                case 't': return TimeLine.CreateFrom(data);
                case 'r': return RepeatLine.CreateFrom(data);
                case 'm': return MediaLine.CreateFrom(data);
                default: return UnknownLine.CreateFrom(data);
            }
        }
    }
}
