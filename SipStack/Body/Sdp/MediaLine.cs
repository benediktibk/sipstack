using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SipStack.Body.Sdp
{
    public class MediaLine : ILine
    {
        private readonly MediaType _mediaType;
        private readonly int _port;
        private readonly int _portCount;
        private readonly MediaTransportProtocol _mediaTransportProtocol;
        private readonly List<string> _mediaFormatDescriptions;

        public MediaLine(MediaType mediaType, int port, int portCount, MediaTransportProtocol mediaTransportProtocol, IReadOnlyList<string> mediaFormatDescriptions)
        {
            _mediaType = mediaType;
            _port = port;
            _portCount = portCount;
            _mediaTransportProtocol = mediaTransportProtocol;
            _mediaFormatDescriptions = mediaFormatDescriptions.ToList();
        }

        public MediaType MediaType => _mediaType;
        public int Port => _port;
        public int PortCount => _portCount;
        public MediaTransportProtocol MediaTransportProtocol => _mediaTransportProtocol;
        public IReadOnlyList<string> MediaFormatDescriptions => _mediaFormatDescriptions;

        public static ParseResult<ILine> Parse(string data)
        {
            var pattern = @"^([^ ]*) ([0-9]*)\/?([0-9]*) ([^ ]*) (.*)$";
            var matches = Regex.Matches(data, pattern);

            if (matches.Count != 1)
                return new ParseResult<ILine>(@"invalid format for media description: {data}");

            var onlyMatch = matches[0];
            var mediaTypeString = onlyMatch.Groups[1].Value;
            var portString = onlyMatch.Groups[2].Value;
            var portCountString = onlyMatch.Groups[3].Value;
            var mediaTransportProtocolString = onlyMatch.Groups[4].Value;
            var mediaFormatDescriptionsString = onlyMatch.Groups[5].Value;
            var mediaFormatDescriptions = new List<string>();
            MediaType mediaType;
            MediaTransportProtocol mediaTransportProtocol;
            int port;
            int portCount = 1;

            if (!MediaTypeUtils.TryParse(mediaTypeString, out mediaType))
                return new ParseResult<ILine>($"invalid value {mediaTypeString} for media type");

            if (!MediaTransportProtocolUtils.TryParse(mediaTransportProtocolString, out mediaTransportProtocol))
                return new ParseResult<ILine>($"invalid value {mediaTransportProtocolString} for media transport protocol");

            if (!int.TryParse(portString, out port))
                return new ParseResult<ILine>($"invalid value {portString} for port");

            if (!string.IsNullOrEmpty(portCountString) && !int.TryParse(portCountString, out portCount))
                return new ParseResult<ILine>($"invalid value {portCountString} for port count");

            pattern = @"[^ ]+";
            matches = Regex.Matches(mediaFormatDescriptionsString, pattern);

            if (matches.Count < 1)
                return new ParseResult<ILine>($"media format description is missing");

            foreach (Match match in matches)
                mediaFormatDescriptions.Add(match.Groups[0].Value);

            return new ParseResult<ILine>(new MediaLine(mediaType, port, portCount, mediaTransportProtocol, mediaFormatDescriptions));
        }
    }
}
