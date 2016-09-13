﻿using System.Collections.Generic;
using System.Linq;

namespace SipStack.Header
{
    public enum HeaderFieldType
    {
        Accept,
        AcceptContact,
        AcceptEncoding,
        AcceptLanguage,
        AcceptResourcePriority,
        AlertInfo,
        Allow,
        AllowEvents,
        AnswerMode,
        AuthenticationInfo,
        Authorization,
        CallId,
        CallInfo,
        Contact,
        ContentDisposition,
        ContentEncoding,
        ContentLanguage,
        ContentLength,
        ContentType,
        Cseq,
        Date,
        Encryption,
        ErrorInfo,
        Event,
        Expires,
        FeatureCaps,
        FlowTimer,
        From,
        Geolocation,
        GeolocationError,
        GeolocationRouting,
        Hide,
        HistoryInfo,
        Identity,
        IdentityInfo,
        InfoPackage,
        InReplyTo,
        Join,
        MaxBreadth,
        MaxForwards,
        MimeVersion,
        MinExpires,
        MinSe,
        Organization,
        PaccessNetworkInfo,
        PanswerState,
        PassertedIdentity,
        PassertedService,
        PassociatedUri,
        PcalledPartyId,
        PchargingFunctionAddresses,
        PchargingVector,
        PdcsTracePartyId,
        PdcsOsps,
        PdcsBillingInfo,
        PdcsLaes,
        PdcsRedirect,
        PearlyMedia,
        PmediaAuthorization,
        PpreferredIdentity,
        PpreferredService,
        PprivateNetworkIndication,
        PprofileKey,
        PrefusedUriList,
        PservedUser,
        PuserDatabase,
        PvisitedNetworkId,
        Path,
        PermissionMissing,
        PolicyContact,
        PolicyId,
        Priority,
        PrivAnswerMode,
        Privacy,
        ProxyAuthenticate,
        ProxyAuthorization,
        ProxyRequire,
        Rack,
        Reason,
        ReasonPhrase,
        RecordRoute,
        RecvInfo,
        ReferEventsAt,
        ReferStub,
        ReferTo,
        ReferredBy,
        RejectContact,
        Replaces,
        ReplyTo,
        RequestDisposition,
        Require,
        ResourcePriority,
        ResponseKey,
        RetryAfter,
        Route,
        Rseq,
        SecurityClient,
        SecurityServer,
        SecurityVerify,
        Server,
        ServiceRoute,
        SessionExpires,
        SessionId,
        SipEtag,
        SipIfMatch,
        Subject,
        SubscriptionState,
        Supported,
        SuppressIfMatch,
        TargetDialog,
        Timestamp,
        To,
        TriggerConstant,
        Unsupported,
        UserAgent,
        UserToUser,
        Via,
        Warning,
        WwwAuthenticate
    }

    public static class HeaderFieldTypeUtils
    {
        private static IDictionary<string, HeaderFieldType> StringToType;
        private static IDictionary<HeaderFieldType, string> TypeToString;
        private static HashSet<HeaderFieldType> HeadersWithOnlyOneValue;

        static HeaderFieldTypeUtils()
        {
            TypeToString = new Dictionary<HeaderFieldType, string>
            {
                { HeaderFieldType.Accept, "Accept" },
                { HeaderFieldType.AcceptContact, "Accept-Contact" },
                { HeaderFieldType.AcceptEncoding, "Accept-Encoding" },
                { HeaderFieldType.AcceptLanguage, "Accept-Language" },
                { HeaderFieldType.AcceptResourcePriority, "Accept-Resource-Priority" },
                { HeaderFieldType.AlertInfo, "Alert-Info" },
                { HeaderFieldType.Allow, "Allow" },
                { HeaderFieldType.AllowEvents, "Allow-Events" },
                { HeaderFieldType.AnswerMode, "Answer-Mode" },
                { HeaderFieldType.AuthenticationInfo, "Authentication-Info" },
                { HeaderFieldType.Authorization, "Authorization" },
                { HeaderFieldType.CallId, "Call-ID" },
                { HeaderFieldType.CallInfo, "Call-Info" },
                { HeaderFieldType.Contact, "Contact" },
                { HeaderFieldType.ContentDisposition, "Content-Disposition" },
                { HeaderFieldType.ContentEncoding, "Content-Encoding" },
                { HeaderFieldType.ContentLanguage, "Content-Language" },
                { HeaderFieldType.ContentLength, "Content-Length" },
                { HeaderFieldType.ContentType, "Content-Type" },
                { HeaderFieldType.Cseq, "CSeq" },
                { HeaderFieldType.Date, "Date" },
                { HeaderFieldType.Encryption, "Encryption" },
                { HeaderFieldType.ErrorInfo, "Error-Info" },
                { HeaderFieldType.Event, "Event" },
                { HeaderFieldType.Expires, "Expires" },
                { HeaderFieldType.FeatureCaps, "Feature-Caps" },
                { HeaderFieldType.FlowTimer, "Flow-Timer" },
                { HeaderFieldType.From, "From" },
                { HeaderFieldType.Geolocation, "Geolocation" },
                { HeaderFieldType.GeolocationError, "Geolocation-Error" },
                { HeaderFieldType.GeolocationRouting, "Geolocation-Routing" },
                { HeaderFieldType.Hide, "Hide" },
                { HeaderFieldType.HistoryInfo, "History-Info" },
                { HeaderFieldType.Identity, "Identity" },
                { HeaderFieldType.IdentityInfo, "Identity-Info" },
                { HeaderFieldType.InfoPackage, "Info-Package" },
                { HeaderFieldType.InReplyTo, "In-Reply-To" },
                { HeaderFieldType.Join, "Join" },
                { HeaderFieldType.MaxBreadth, "Max-Breadth" },
                { HeaderFieldType.MaxForwards, "Max-Forwards" },
                { HeaderFieldType.MimeVersion, "MIME-Version" },
                { HeaderFieldType.MinExpires, "Min-Expires" },
                { HeaderFieldType.MinSe, "Min-SE" },
                { HeaderFieldType.Organization, "Organization" },
                { HeaderFieldType.PaccessNetworkInfo, "P-Access-Network-Info" },
                { HeaderFieldType.PanswerState, "P-Answer-State" },
                { HeaderFieldType.PassertedIdentity, "P-Asserted-Identity" },
                { HeaderFieldType.PassertedService, "P-Asserted-Service" },
                { HeaderFieldType.PassociatedUri, "P-Associated-URI" },
                { HeaderFieldType.PcalledPartyId, "P-Called-Party-ID" },
                { HeaderFieldType.PchargingFunctionAddresses, "P-Charging-Function-Addresses" },
                { HeaderFieldType.PchargingVector, "P-Charging-Vector" },
                { HeaderFieldType.PdcsTracePartyId, "P-DCS-Trace-Party-ID" },
                { HeaderFieldType.PdcsOsps, "P-DCS-OSPS" },
                { HeaderFieldType.PdcsBillingInfo, "P-DCS-Billing-Info" },
                { HeaderFieldType.PdcsLaes, "P-DCS-LAES" },
                { HeaderFieldType.PdcsRedirect, "P-DCS-Redirect" },
                { HeaderFieldType.PearlyMedia, "P-Early-Media" },
                { HeaderFieldType.PmediaAuthorization, "P-Media-Authorization" },
                { HeaderFieldType.PpreferredIdentity, "P-Preferred-Identity" },
                { HeaderFieldType.PpreferredService, "P-Preferred-Service" },
                { HeaderFieldType.PprivateNetworkIndication, "P-Private-Network-Indication" },
                { HeaderFieldType.PprofileKey, "P-Profile-Key" },
                { HeaderFieldType.PrefusedUriList, "P-Refused-URI-List" },
                { HeaderFieldType.PservedUser, "P-Served-User" },
                { HeaderFieldType.PuserDatabase, "P-User-Database" },
                { HeaderFieldType.PvisitedNetworkId, "P-Visited-Network-ID" },
                { HeaderFieldType.Path, "Path" },
                { HeaderFieldType.PermissionMissing, "Permission-Missing" },
                { HeaderFieldType.PolicyContact, "Policy-Contact" },
                { HeaderFieldType.PolicyId, "Policy-ID" },
                { HeaderFieldType.Priority, "Priority" },
                { HeaderFieldType.PrivAnswerMode, "Priv-Answer-Mode" },
                { HeaderFieldType.Privacy, "Privacy" },
                { HeaderFieldType.ProxyAuthenticate, "Proxy-Authenticate" },
                { HeaderFieldType.ProxyAuthorization, "Proxy-Authorization" },
                { HeaderFieldType.ProxyRequire, "Proxy-Require" },
                { HeaderFieldType.Rack, "RAck" },
                { HeaderFieldType.Reason, "Reason" },
                { HeaderFieldType.ReasonPhrase, "Reason-Phrase" },
                { HeaderFieldType.RecordRoute, "Record-Route" },
                { HeaderFieldType.RecvInfo, "Recv-Info" },
                { HeaderFieldType.ReferEventsAt, "Refer-Events-At" },
                { HeaderFieldType.ReferStub, "Refer-Sub" },
                { HeaderFieldType.ReferTo, "Refer-To" },
                { HeaderFieldType.ReferredBy, "Referred-By" },
                { HeaderFieldType.RejectContact, "Reject-Contact" },
                { HeaderFieldType.Replaces, "Replaces" },
                { HeaderFieldType.ReplyTo, "Reply-To" },
                { HeaderFieldType.RequestDisposition, "Request-Disposition" },
                { HeaderFieldType.Require, "Require" },
                { HeaderFieldType.ResourcePriority, "Resource-Priority" },
                { HeaderFieldType.ResponseKey, "Response-Key" },
                { HeaderFieldType.RetryAfter, "Retry-After" },
                { HeaderFieldType.Route, "Route" },
                { HeaderFieldType.Rseq, "RSeq" },
                { HeaderFieldType.SecurityClient, "Security-Client" },
                { HeaderFieldType.SecurityServer, "Security-Server" },
                { HeaderFieldType.SecurityVerify, "Security-Verify" },
                { HeaderFieldType.Server, "Server" },
                { HeaderFieldType.ServiceRoute, "Service-Route" },
                { HeaderFieldType.SessionExpires, "Session-Expires" },
                { HeaderFieldType.SessionId, "Session-ID" },
                { HeaderFieldType.SipEtag, "SIP-ETag" },
                { HeaderFieldType.SipIfMatch, "SIP-If-Match" },
                { HeaderFieldType.Subject, "Subject" },
                { HeaderFieldType.SubscriptionState, "Subscription-State" },
                { HeaderFieldType.Supported, "Supported" },
                { HeaderFieldType.SuppressIfMatch, "Suppress-If-Match" },
                { HeaderFieldType.TargetDialog, "Target-Dialog" },
                { HeaderFieldType.Timestamp, "Timestamp" },
                { HeaderFieldType.To, "To" },
                { HeaderFieldType.TriggerConstant, "Trigger-Consent" },
                { HeaderFieldType.Unsupported, "Unsupported" },
                { HeaderFieldType.UserAgent, "User-Agent" },
                { HeaderFieldType.UserToUser, "User-to-User" },
                { HeaderFieldType.Via, "Via" },
                { HeaderFieldType.Warning, "Warning" },
                { HeaderFieldType.WwwAuthenticate, "WWW-Authenticate" }
            };

            HeadersWithOnlyOneValue = new HashSet<HeaderFieldType>
            {
                HeaderFieldType.CallId,
                HeaderFieldType.ContentLength,
                HeaderFieldType.ContentType,
                HeaderFieldType.Cseq,
                HeaderFieldType.Date,
                HeaderFieldType.Expires,
                HeaderFieldType.From,
                HeaderFieldType.MaxForwards,
                HeaderFieldType.SessionId,
                HeaderFieldType.To
            };

            StringToType = TypeToString.ToDictionary(x => x.Value.ToLower(), x => x.Key);
        }

        public static bool TryParse(string value, out HeaderFieldType requestMethod)
        {
            return StringToType.TryGetValue(value.ToLower(), out requestMethod);
        }

        public static string ToFriendlyString(this HeaderFieldType value)
        {
            return TypeToString[value];
        }

        public static bool CanHaveMultipleValues(this HeaderFieldType value)
        {
            return !HeadersWithOnlyOneValue.Contains(value);
        }
    }
}
