using SipStack.Utils;
using System;
using System.Collections.Generic;

namespace SipStack.Body.Sdp
{
    public static class LineParsers
    {
        public static Func<string, ParseResult<Attribute>> Attribute = (string data) => { return Sdp.Attribute.Parse(data); };
        public static Func<string, ParseResult<HttpUri>> HttpUri = (string data) => { return Sdp.HttpUri.Parse(data); };
        public static Func<string, ParseResult<Bandwidth>> Bandwidth = (string data) => { return Sdp.Bandwidth.Parse(data); };
        public static Func<string, ParseResult<ConnectionInformation>> ConnectionInformation = (string data) => { return Sdp.ConnectionInformation.Parse(data); };
        public static Func<string, ParseResult<EncryptionKey>> EncryptionKey = (string data) => { return Sdp.EncryptionKey.Parse(data); };
        public static Func<string, ParseResult<Media>> Media = (string data) => { return Sdp.Media.Parse(data); };
        public static Func<string, ParseResult<Originator>> Originator = (string data) => { return Sdp.Originator.Parse(data); };
        public static Func<string, ParseResult<Repeat>> Repeat = (string data) => { return Sdp.Repeat.Parse(data); };
        public static Func<string, ParseResult<Timing>> Timing = (string data) => { return Sdp.Timing.Parse(data); };
        public static Func<string, ParseResult<Version>> Version = (string data) => { return Sdp.Version.Parse(data); };
        public static Func<string, ParseResult<string>> SessionName = (string data) => { return ParseResult<string>.CreateSuccess(data); };
        public static Func<string, ParseResult<string>> Description = (string data) => { return ParseResult<string>.CreateSuccess(data); };
        public static Func<string, ParseResult<EmailAddress>> EmailAddress = (string data) => { return Utils.EmailAddress.Parse(data); };
        public static Func<string, ParseResult<PhoneNumber>> PhoneNumber = (string data) => { return Utils.PhoneNumber.Parse(data); };
        public static Func<string, ParseResult<List<TimeZoneAdjustment>>> TimeZoneAdjustment = (string data) => { return Sdp.TimeZoneAdjustment.Parse(data); };
    }
}
