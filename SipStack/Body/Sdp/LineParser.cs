using SipStack.Utils;
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
            return Parse(type[0], data);
        }

        private ParseResult<ILine> Parse(char type, string data)
        {
            switch (type)
            {
                case 'v': return VersionLine.Parse(data);
                case 'o': return OriginatorLine.Parse(data);
                case 's': return SessionNameLine.Parse(data);
                case 'i': return DescriptionLine.Parse(data);
                case 'u': return UriLine.Parse(data);
                case 'e': return EmailAddressLine.Parse(data);
                case 'p': return PhoneNumberLine.Parse(data);
                case 'c': return ConnectionInformationLine.Parse(data);
                case 'b': return BandwidthLine.Parse(data);
                case 'z': return TimeZoneLine.Parse(data);
                case 'k': return EncryptionKeyLine.Parse(data);
                case 'a': return AttributeLine.Parse(data);
                case 't': return TimeLine.Parse(data);
                case 'r': return RepeatLine.Parse(data);
                case 'm': return MediaLine.Parse(data);
                default: return UnknownLine.Parse(data);
            }
        }
    }
}
