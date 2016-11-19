using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public class SdpBody : IBody
    {
        #region private variables

        private readonly List<Bandwidth> _bandwidths;
        private readonly List<TimeDescription> _timeDescriptions;

        #endregion

        #region constructors

        public SdpBody(
            int protocolVersion, Originator originator, string sessionName, string sessionDescription, Uri sessionUri, 
            EmailAddress emailAddress, PhoneNumber phoneNumber, ConnectionInformation connectionInformation, 
            IReadOnlyList<Bandwidth> bandwidths, IReadOnlyList<TimeDescription> timeDescriptions, TimeZoneAdjustment timeZoneAdjustment,
            EncryptionKey encryptionKey)
        {
            ProtocolVersion = protocolVersion;
            Originator = originator;
            SessionName = sessionName;
            SessionDescription = sessionDescription;
            SessionUri = sessionUri;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            ConnectionInformation = connectionInformation;
            _bandwidths = bandwidths.ToList();
            _timeDescriptions = timeDescriptions.ToList();
            TimeZoneAdjustment = timeZoneAdjustment;
            EncryptionKey = encryptionKey;
        }

        #endregion

        #region properties

        public int ProtocolVersion { get; }
        public Originator Originator { get; }
        public string SessionName { get; }
        public string SessionDescription { get; }
        public Uri SessionUri { get; }
        public EmailAddress EmailAddress { get; }
        public PhoneNumber PhoneNumber { get; }
        public ConnectionInformation ConnectionInformation { get; }
        public IReadOnlyList<Bandwidth> Bandwidth => _bandwidths;
        public IReadOnlyList<TimeDescription> TimeDescriptions => _timeDescriptions;
        public TimeZoneAdjustment TimeZoneAdjustment { get; }
        public EncryptionKey EncryptionKey { get; }
        

        public int ContentLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region public functions

        public void AddTo(MessageBuilder messageBuilder)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
