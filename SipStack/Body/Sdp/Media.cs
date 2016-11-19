using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public class Media
    {
        private readonly List<string> _mediaFormatDescriptions;

        public Media(MediaType mediaType, int port, int portCount, MediaTransportProtocol mediaTransportProtocol, IReadOnlyList<string> mediaFormatDescriptions)
        {
            MediaType = mediaType;
            Port = port;
            PortCount = portCount;
            MediaTransportProtocol = mediaTransportProtocol;
            _mediaFormatDescriptions = mediaFormatDescriptions.ToList();
        }

        public MediaType MediaType { get; }
        public int Port { get; }
        public int PortCount { get; }
        public MediaTransportProtocol MediaTransportProtocol { get; }
        public IReadOnlyList<string> MediaFormatDescriptions => _mediaFormatDescriptions;
    }
}
