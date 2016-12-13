using System.Collections.Generic;

namespace SipStack.Header
{
    public class HeaderDto
    {
        #region constructors

        public HeaderDto()
        {
            Accept = new List<string>();
            AcceptContact = new List<string>();
            AcceptEncoding = new List<string>();
            AcceptLanguage = new List<string>();
            AcceptResourcePriority = new List<string>();
            AlertInfo = new List<string>();
            Allow = new List<RequestMethod>();
            AllowEvents = new List<string>();
            AnswerMode = new List<string>();
            AuthenticationInfo = new List<string>();
            Authorization = new List<string>();
            CallId = new List<string>();
            CallInfo = new List<string>();
            Contact = new List<string>();
            ContentDisposition = new List<string>();
            ContentEncoding = new List<string>();
            ContentLanguage = new List<string>();
            Cseq = new List<string>();
            Date = new List<string>();
            Encryption = new List<string>();
            ErrorInfo = new List<string>();
            Event = new List<string>();
            Expires = new List<string>();
            FeatureCaps = new List<string>();
            FlowTimer = new List<string>();
            From = new List<string>();
            Geolocation = new List<string>();
            GeolocationError = new List<string>();
            GeolocationRouting = new List<string>();
            Hide = new List<string>();
            HistoryInfo = new List<string>();
            Identity = new List<string>();
            IdentityInfo = new List<string>();
            InfoPackage = new List<string>();
            InReplyTo = new List<string>();
            Join = new List<string>();
            MaxBreadth = new List<string>();
            MaxForwards = new List<string>();
            MimeVersion = new List<string>();
            MinExpires = new List<string>();
            MinSe = new List<string>();
            Organization = new List<string>();
            PaccessNetworkInfo = new List<string>();
            PanswerState = new List<string>();
            PassertedIdentity = new List<string>();
            PassertedService = new List<string>();
            PassociatedUri = new List<string>();
            PcalledPartyId = new List<string>();
            PchargingFunctionAddresses = new List<string>();
            PchargingVector = new List<string>();
            PdcsTracePartyId = new List<string>();
            PdcsOsps = new List<string>();
            PdcsBillingInfo = new List<string>();
            PdcsLaes = new List<string>();
            PdcsRedirect = new List<string>();
            PearlyMedia = new List<string>();
            PmediaAuthorization = new List<string>();
            PpreferredIdentity = new List<string>();
            PpreferredService = new List<string>();
            PprivateNetworkIndication = new List<string>();
            PprofileKey = new List<string>();
            PrefusedUriList = new List<string>();
            PservedUser = new List<string>();
            PuserDatabase = new List<string>();
            PvisitedNetworkId = new List<string>();
            Path = new List<string>();
            PermissionMissing = new List<string>();
            PolicyContact = new List<string>();
            PolicyId = new List<string>();
            Priority = new List<string>();
            PrivAnswerMode = new List<string>();
            Privacy = new List<string>();
            ProxyAuthenticate = new List<string>();
            ProxyAuthorization = new List<string>();
            ProxyRequire = new List<string>();
            Rack = new List<string>();
            Reason = new List<string>();
            ReasonPhrase = new List<string>();
            RecordRoute = new List<string>();
            RecvInfo = new List<string>();
            ReferEventsAt = new List<string>();
            ReferStub = new List<string>();
            ReferTo = new List<string>();
            ReferredBy = new List<string>();
            RejectContact = new List<string>();
            Replaces = new List<string>();
            ReplyTo = new List<string>();
            RequestDisposition = new List<string>();
            Require = new List<string>();
            ResourcePriority = new List<string>();
            ResponseKey = new List<string>();
            RetryAfter = new List<string>();
            Route = new List<string>();
            Rseq = new List<string>();
            SecurityClient = new List<string>();
            SecurityServer = new List<string>();
            SecurityVerify = new List<string>();
            Server = new List<string>();
            ServiceRoute = new List<string>();
            SessionExpires = new List<string>();
            SessionId = new List<string>();
            SipEtag = new List<string>();
            SipIfMatch = new List<string>();
            Subject = new List<string>();
            SubscriptionState = new List<string>();
            Supported = new List<string>();
            SuppressIfMatch = new List<string>();
            TargetDialog = new List<string>();
            Timestamp = new List<string>();
            To = new List<string>();
            TriggerConstant = new List<string>();
            Unsupported = new List<string>();
            UserAgent = new List<string>();
            UserToUser = new List<string>();
            Via = new List<string>();
            Warning = new List<string>();
            WwwAuthenticate = new List<string>();
            CustomHeaders = new List<HeaderField>();
        }

        #endregion

        #region properties

        public IMethod Method { get; set; }
        public List<string> Accept { get; set; }
        public List<string> AcceptContact { get; set; }
        public List<string> AcceptEncoding { get; set; }
        public List<string> AcceptLanguage { get; set; }
        public List<string> AcceptResourcePriority { get; set; }
        public List<string> AlertInfo { get; set; }
        public List<RequestMethod> Allow { get; set; }
        public List<string> AllowEvents { get; set; }
        public List<string> AnswerMode { get; set; }
        public List<string> AuthenticationInfo { get; set; }
        public List<string> Authorization { get; set; }
        public List<string> CallId { get; set; }
        public List<string> CallInfo { get; set; }
        public List<string> Contact { get; set; }
        public List<string> ContentDisposition { get; set; }
        public List<string> ContentEncoding { get; set; }
        public List<string> ContentLanguage { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
        public List<string> Cseq { get; set; }
        public List<string> Date { get; set; }
        public List<string> Encryption { get; set; }
        public List<string> ErrorInfo { get; set; }
        public List<string> Event { get; set; }
        public List<string> Expires { get; set; }
        public List<string> FeatureCaps { get; set; }
        public List<string> FlowTimer { get; set; }
        public List<string> From { get; set; }
        public List<string> Geolocation { get; set; }
        public List<string> GeolocationError { get; set; }
        public List<string> GeolocationRouting { get; set; }
        public List<string> Hide { get; set; }
        public List<string> HistoryInfo { get; set; }
        public List<string> Identity { get; set; }
        public List<string> IdentityInfo { get; set; }
        public List<string> InfoPackage { get; set; }
        public List<string> InReplyTo { get; set; }
        public List<string> Join { get; set; }
        public List<string> MaxBreadth { get; set; }
        public List<string> MaxForwards { get; set; }
        public List<string> MimeVersion { get; set; }
        public List<string> MinExpires { get; set; }
        public List<string> MinSe { get; set; }
        public List<string> Organization { get; set; }
        public List<string> PaccessNetworkInfo { get; set; }
        public List<string> PanswerState { get; set; }
        public List<string> PassertedIdentity { get; set; }
        public List<string> PassertedService { get; set; }
        public List<string> PassociatedUri { get; set; }
        public List<string> PcalledPartyId { get; set; }
        public List<string> PchargingFunctionAddresses { get; set; }
        public List<string> PchargingVector { get; set; }
        public List<string> PdcsTracePartyId { get; set; }
        public List<string> PdcsOsps { get; set; }
        public List<string> PdcsBillingInfo { get; set; }
        public List<string> PdcsLaes { get; set; }
        public List<string> PdcsRedirect { get; set; }
        public List<string> PearlyMedia { get; set; }
        public List<string> PmediaAuthorization { get; set; }
        public List<string> PpreferredIdentity { get; set; }
        public List<string> PpreferredService { get; set; }
        public List<string> PprivateNetworkIndication { get; set; }
        public List<string> PprofileKey { get; set; }
        public List<string> PrefusedUriList { get; set; }
        public List<string> PservedUser { get; set; }
        public List<string> PuserDatabase { get; set; }
        public List<string> PvisitedNetworkId { get; set; }
        public List<string> Path { get; set; }
        public List<string> PermissionMissing { get; set; }
        public List<string> PolicyContact { get; set; }
        public List<string> PolicyId { get; set; }
        public List<string> Priority { get; set; }
        public List<string> PrivAnswerMode { get; set; }
        public List<string> Privacy { get; set; }
        public List<string> ProxyAuthenticate { get; set; }
        public List<string> ProxyAuthorization { get; set; }
        public List<string> ProxyRequire { get; set; }
        public List<string> Rack { get; set; }
        public List<string> Reason { get; set; }
        public List<string> ReasonPhrase { get; set; }
        public List<string> RecordRoute { get; set; }
        public List<string> RecvInfo { get; set; }
        public List<string> ReferEventsAt { get; set; }
        public List<string> ReferStub { get; set; }
        public List<string> ReferTo { get; set; }
        public List<string> ReferredBy { get; set; }
        public List<string> RejectContact { get; set; }
        public List<string> Replaces { get; set; }
        public List<string> ReplyTo { get; set; }
        public List<string> RequestDisposition { get; set; }
        public List<string> Require { get; set; }
        public List<string> ResourcePriority { get; set; }
        public List<string> ResponseKey { get; set; }
        public List<string> RetryAfter { get; set; }
        public List<string> Route { get; set; }
        public List<string> Rseq { get; set; }
        public List<string> SecurityClient { get; set; }
        public List<string> SecurityServer { get; set; }
        public List<string> SecurityVerify { get; set; }
        public List<string> Server { get; set; }
        public List<string> ServiceRoute { get; set; }
        public List<string> SessionExpires { get; set; }
        public List<string> SessionId { get; set; }
        public List<string> SipEtag { get; set; }
        public List<string> SipIfMatch { get; set; }
        public List<string> Subject { get; set; }
        public List<string> SubscriptionState { get; set; }
        public List<string> Supported { get; set; }
        public List<string> SuppressIfMatch { get; set; }
        public List<string> TargetDialog { get; set; }
        public List<string> Timestamp { get; set; }
        public List<string> To { get; set; }
        public List<string> TriggerConstant { get; set; }
        public List<string> Unsupported { get; set; }
        public List<string> UserAgent { get; set; }
        public List<string> UserToUser { get; set; }
        public List<string> Via { get; set; }
        public List<string> Warning { get; set; }
        public List<string> WwwAuthenticate { get; set; }
        public List<HeaderField> CustomHeaders { get; set; }

        #endregion
    }
}
