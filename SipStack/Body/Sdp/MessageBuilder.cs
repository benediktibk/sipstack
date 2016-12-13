using SipStack.Network;
using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SipStack.Body.Sdp
{
    public class MessageBuilder
    {
        #region variables

        private readonly SipStack.MessageBuilder _messageBuilder;

        #endregion

        #region constructors

        public MessageBuilder(SipStack.MessageBuilder messageBuilder)
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
            _messageBuilder.AddLineFormat("s={0}", value);
        }

        public void AddSessionDescription(string value)
        {
            _messageBuilder.AddLineFormat("i={0}", value);
        }

        public void AddSessionUri(Uri value)
        {
            _messageBuilder.AddLineFormat("u={0}", value.ToString());
        }

        public void AddEmailAddress(EmailAddress value)
        {
            _messageBuilder.AddLineFormat("e={0}", value.ToString());
        }

        public void AddPhoneNumber(PhoneNumber value)
        {
            _messageBuilder.AddLineFormat("p={0}", value.ToString());
        }

        public void AddConnectionInformation(ConnectionInformation value)
        {
            var netType = NetTypeUtils.ToFriendlyString(value.NetType);
            var addressType = AddressTypeUtils.ToFriendlyString(value.AddressType);

            if (value.IsUnicast)
                _messageBuilder.AddLineFormat("c={0} {1} {2}", netType, addressType, value.Host);
            else if (value.AddressType == AddressType.Ipv4)
            {
                if (value.NumberOfMulticastAddresses == 1)
                    _messageBuilder.AddLineFormat("c={0} {1} {2}/{3}", netType, addressType, value.Host, value.MulticastTimeToLive.ToString());
                else
                    _messageBuilder.AddLineFormat("c={0} {1} {2}/{3}/{4}", netType, addressType, value.Host, value.MulticastTimeToLive.ToString(), value.NumberOfMulticastAddresses.ToString());
            }
            else
            {
                if (value.NumberOfMulticastAddresses == 1)
                    _messageBuilder.AddLineFormat("c={0} {1} {2}", netType, addressType, value.Host);
                else
                    _messageBuilder.AddLineFormat("c={0} {1} {2}/{3}", netType, addressType, value.Host, value.NumberOfMulticastAddresses.ToString());
            }
        }

        public void AddBandwidth(Bandwidth value)
        {
            if (value.Type == BandwidthType.Unknown)
                throw new ArgumentOutOfRangeException("value", "the bandwidth type must not be unkown");

            _messageBuilder.AddLineFormat("b={0}:{1}", BandwidthTypeUtils.ToFriendlyString(value.Type), value.Amount.ToString());
        }

        public void AddTiming(Timing value)
        {
            var start = DateTimeHelper.DateTimeToNtpTimeStamp(value.Start).ToString();
            var end = value.End == DateTime.MaxValue ? "0"  : DateTimeHelper.DateTimeToNtpTimeStamp(value.End).ToString();
            _messageBuilder.AddLineFormat("t={0} {1}", start, end);
        }

        public void AddRepeat(Repeat value)
        {
            _messageBuilder.AddLineFormat(
                "r={0} {1} {2} {3}", 
                value.RepeatInterval.TotalSeconds.ToString(), 
                value.ActiveDuration.TotalSeconds.ToString(), 
                value.OffsetStart.TotalSeconds.ToString(), 
                value.OffsetEnd.TotalSeconds.ToString());
        }

        public void AddTimeZoneAdjustment(IReadOnlyList<TimeZoneAdjustment> values)
        {
            if (values.Count == 0)
                return;

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("{0} {1}", values[0].Time.ToString(), values[0].Offset.ToString());

            foreach (var value in values.Skip(1))
                stringBuilder.AppendFormat(" {0} {1}", value.Time.ToString(), value.Offset.ToString());

            _messageBuilder.AddLineFormat("z={0}", stringBuilder.ToString());
        }

        public void AddEncryptionKey(EncryptionKey value)
        {
            var keyTypeString = EncryptionKeyTypeUtils.ToFriendlyString(value.KeyType);

            if (value.KeyType == EncryptionKeyType.Prompt)
                _messageBuilder.AddLineFormat("k={0}", keyTypeString);
            else
                _messageBuilder.AddLineFormat("k={0}:{1}", keyTypeString, value.Key);
        }

        public void AddAttribute(Attribute value)
        {
            if (value.IsFlag)
                _messageBuilder.AddLineFormat("a={0}", value.Name);
            else
                _messageBuilder.AddLineFormat("a={0}:{1}", value.Name, value.Value);
        }

        public void AddMedia(Media value)
        {
            var stringBuilder = new StringBuilder();            
                
            foreach (var mediaFormatDescription in value.MediaFormatDescriptions)
                stringBuilder.AppendFormat(" {0}", mediaFormatDescription);

            if (value.PortCount == 1)
                _messageBuilder.AddLineFormat(
                    "m={0} {1} {2}{3}",
                    MediaTypeUtils.ToFriendlyString(value.MediaType),
                    value.Port.ToString(),
                    MediaTransportProtocolUtils.ToFriendlyString(value.MediaTransportProtocol),
                    stringBuilder.ToString());
            else
                _messageBuilder.AddLineFormat(
                    "m={0} {1}/{2} {3}{4}",
                    MediaTypeUtils.ToFriendlyString(value.MediaType),
                    value.Port.ToString(),
                    value.PortCount.ToString(),
                    MediaTransportProtocolUtils.ToFriendlyString(value.MediaTransportProtocol),
                    stringBuilder.ToString());
        }

        #endregion
    }
}
