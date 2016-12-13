using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public class MediaDescription
    {
        public MediaDescription(Media media, string title, IEnumerable<ConnectionInformation> connectionInformation, IEnumerable<Bandwidth> bandwidths, EncryptionKey encryptionKey, IEnumerable<Attribute> attributes)
        {
            if (media == null)
                throw new ArgumentNullException("media");
            if (connectionInformation == null)
                throw new ArgumentNullException("connectionInformation");
            if (bandwidths == null)
                throw new ArgumentNullException("bandwidths");
            if (attributes == null)
                throw new ArgumentNullException("attributes");

            Media = media;
            Title = title;
            ConnectionInformation = connectionInformation.ToList();
            Bandwidths = bandwidths.ToList();
            EncryptionKey = encryptionKey;
            Attributes = attributes.ToList();
        }

        public Media Media { get; }
        public string Title { get; }
        public IReadOnlyList<ConnectionInformation> ConnectionInformation { get; }
        public IReadOnlyList<Bandwidth> Bandwidths { get; }
        public EncryptionKey EncryptionKey { get; }
        public IReadOnlyList<Attribute> Attributes { get; }
    }
}
