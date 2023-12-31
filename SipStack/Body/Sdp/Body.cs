﻿using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Body.Sdp
{
    public class Body : IBody
    {
        #region constructors

        public Body(
            int protocolVersion, Originator originator, string sessionName, string sessionDescription, Uri sessionUri, 
            EmailAddress emailAddress, PhoneNumber phoneNumber, IEnumerable<ConnectionInformation> connectionInformation,
            IEnumerable<Bandwidth> bandwidths, IEnumerable<TimeDescription> timeDescriptions, IEnumerable<TimeZoneAdjustment> timeZoneAdjustments,
            EncryptionKey encryptionKey, IEnumerable<Attribute> attributes, IEnumerable<MediaDescription> mediaDescriptions)
        {
            if (originator == null)
                throw new ArgumentNullException("originator");
            if (sessionName == null)
                throw new ArgumentNullException("sessionName");
            if (connectionInformation == null)
                throw new ArgumentNullException("connectionInformation");
            if (bandwidths == null)
                throw new ArgumentNullException("bandwidths");
            if (timeDescriptions == null)
                throw new ArgumentNullException("timeDescriptions");
            if (timeZoneAdjustments == null)
                throw new ArgumentNullException("timeZoneAdjustments");
            if (attributes == null)
                throw new ArgumentNullException("attributes");
            if (mediaDescriptions == null)
                throw new ArgumentNullException("mediaDescriptions");

            ProtocolVersion = protocolVersion;
            Originator = originator;
            SessionName = sessionName;
            SessionDescription = sessionDescription;
            SessionUri = sessionUri;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            ConnectionInformation = connectionInformation.ToList();
            Bandwidths = bandwidths.ToList();
            TimeDescriptions = timeDescriptions.ToList();
            TimeZoneAdjustments = timeZoneAdjustments.ToList();
            EncryptionKey = encryptionKey;
            Attributes = attributes.ToList();
            MediaDescriptions = mediaDescriptions.ToList();
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
        public IReadOnlyList<ConnectionInformation> ConnectionInformation { get; }
        public IReadOnlyList<Bandwidth> Bandwidths { get; }
        public IReadOnlyList<TimeDescription> TimeDescriptions { get; }
        public IReadOnlyList<TimeZoneAdjustment> TimeZoneAdjustments { get; }
        public EncryptionKey EncryptionKey { get; }
        public IReadOnlyList<Attribute> Attributes { get; }
        public IReadOnlyList<MediaDescription> MediaDescriptions { get; }

        #endregion

        #region public functions

        public void AddTo(SipStack.MessageBuilder messageBuilder)
        {
            var sdpMessageBuilder = new MessageBuilder(messageBuilder);

            sdpMessageBuilder.AddProtocolVersion(ProtocolVersion);
            sdpMessageBuilder.AddOriginator(Originator);
            sdpMessageBuilder.AddSessionName(SessionName);

            if (SessionDescription != null)
                sdpMessageBuilder.AddSessionDescription(SessionDescription);

            if (SessionUri != null)
                sdpMessageBuilder.AddSessionUri(SessionUri);

            if (EmailAddress != null)
                sdpMessageBuilder.AddEmailAddress(EmailAddress);

            if (PhoneNumber != null)
                sdpMessageBuilder.AddPhoneNumber(PhoneNumber);

            foreach (var connectionInformation in ConnectionInformation)
                sdpMessageBuilder.AddConnectionInformation(connectionInformation);

            foreach (var bandwidth in Bandwidths.Where(x => x.Type != BandwidthType.Unknown))
                sdpMessageBuilder.AddBandwidth(bandwidth);

            foreach (var timeDescription in TimeDescriptions)
            {
                sdpMessageBuilder.AddTiming(timeDescription.Time);

                foreach (var repeat in timeDescription.Repeatings)
                    sdpMessageBuilder.AddRepeat(repeat);
            }

            if (TimeZoneAdjustments.Count() > 0)
                sdpMessageBuilder.AddTimeZoneAdjustment(TimeZoneAdjustments);

            if (EncryptionKey != null)
                sdpMessageBuilder.AddEncryptionKey(EncryptionKey);

            foreach (var attribute in Attributes)
                sdpMessageBuilder.AddAttribute(attribute);

            foreach (var mediaDescription in MediaDescriptions)
            {
                sdpMessageBuilder.AddMedia(mediaDescription.Media);

                if (mediaDescription.Title != null)
                    sdpMessageBuilder.AddSessionDescription(mediaDescription.Title);

                foreach (var connectionInformation in mediaDescription.ConnectionInformation)
                    sdpMessageBuilder.AddConnectionInformation(connectionInformation);

                foreach (var bandwidth in mediaDescription.Bandwidths)
                    sdpMessageBuilder.AddBandwidth(bandwidth);

                if (mediaDescription.EncryptionKey != null)
                    sdpMessageBuilder.AddEncryptionKey(mediaDescription.EncryptionKey);

                foreach (var attribute in mediaDescription.Attributes)
                    sdpMessageBuilder.AddAttribute(attribute);
            }
        }

        #endregion
    }
}
