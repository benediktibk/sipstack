using SipStack.Network;
using SipStack.Utils;
using System;
using System.Collections.Generic;

namespace SipStack.Body.Sdp
{
    public class SdpMessageBuilder
    {
        #region variables

        private readonly MessageBuilder _messageBuilder;

        #endregion

        #region constructors

        public SdpMessageBuilder(MessageBuilder messageBuilder)
        {
            _messageBuilder = messageBuilder;
        }

        #endregion

        #region public functions

        public void AddProtocolVersion(int value)
        {
            _messageBuilder.AddLineFormat("v={0}", value.ToString());
        }

        public void AddOriginator(Originator value)
        {
            _messageBuilder.AddLineFormat(
                "o={0} {1} {2} {3} {4} {5}",
                value.Username, value.SessionId.ToString(), value.SessionVersion.ToString(),
                NetTypeUtils.ToFriendlyString(value.NetType), AddressTypeUtils.ToFriendlyString(value.AddressType),
                value.Host.ToString());
        }

        public void AddSessionName(string value)
        {
            throw new NotImplementedException();
        }

        public void AddSessionDescription(string value)
        {
            throw new NotImplementedException();
        }

        public void AddSessionUri(Uri value)
        {
            throw new NotImplementedException();
        }

        public void AddEmailAddress(EmailAddress value)
        {
            throw new NotImplementedException();
        }

        public void AddPhoneNumber(PhoneNumber value)
        {
            throw new NotImplementedException();
        }

        public void AddConnectionInformation(ConnectionInformation value)
        {
            throw new NotImplementedException();
        }

        public void AddBandwidth(Bandwidth value)
        {
            throw new NotImplementedException();
        }

        public void AddTiming(Timing value)
        {
            throw new NotImplementedException();
        }

        public void AddRepeat(Repeat value)
        {
            throw new NotImplementedException();
        }

        public void AddTimeZoneAdjustment(IReadOnlyList<TimeZoneAdjustment> values)
        {
            throw new NotImplementedException();
        }

        public void AddEncryptionKey(EncryptionKey value)
        {
            throw new NotImplementedException();
        }

        public void AddAttribute(Attribute value)
        {
            throw new NotImplementedException();
        }

        public void AddMedia(Media value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
