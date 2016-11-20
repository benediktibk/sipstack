using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack.Body.Sdp
{
    public class MediaDescription
    {
        private readonly List<Bandwidth> _bandwidths;
        private readonly List<Attribute> _attributes;

        public MediaDescription(Media media, string title, ConnectionInformation connectionInformation, IReadOnlyList<Bandwidth> bandwidths, EncryptionKey encryptionKey, IReadOnlyList<Attribute> attributes)
        {
            Media = media;
            Title = title;
            ConnectionInformation = connectionInformation;
            _bandwidths = bandwidths.ToList();
            EncryptionKey = encryptionKey;
            _attributes = attributes.ToList();
        }

        public Media Media { get; }
        public string Title { get; }
        public ConnectionInformation ConnectionInformation { get; }
        public IReadOnlyList<Bandwidth> Bandwidths => _bandwidths;
        public EncryptionKey EncryptionKey { get; }
        public IReadOnlyList<Attribute> Attributes => _attributes;
    }
}
