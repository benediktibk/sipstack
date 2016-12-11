using System;
using System.Collections.Generic;

namespace SipStack.Header
{
    public class Header
    {
        #region constructors

        public Header(HeaderDto header)
        {
            Method = header.Method;
            Accept = header.Accept;
            AcceptContact = header.AcceptContact;
            AcceptEncoding = header.AcceptEncoding;
            AcceptLanguage = header.AcceptLanguage;
            AcceptResourcePriority = header.AcceptResourcePriority;
            AlertInfo = header.AlertInfo;
            Allow = header.Allow;
            AllowEvents = header.AllowEvents;
            AnswerMode = header.AnswerMode;
            AuthenticationInfo = header.AuthenticationInfo;
            Authorization = header.Authorization;
            CallId = header.CallId;
            CallInfo = header.CallInfo;
            Contact = header.Contact;
            ContentDisposition = header.ContentDisposition;
            ContentEncoding = header.ContentEncoding;
            ContentLanguage = header.ContentLanguage;
            ContentLength = header.ContentLength;
            ContentType = header.ContentType;
            Cseq = header.Cseq;
            Date = header.Date;
            Encryption = header.Encryption;
            ErrorInfo = header.ErrorInfo;
            Event = header.Event;
            Expires = header.Expires;
            FeatureCaps = header.FeatureCaps;
            FlowTimer = header.FlowTimer;
            From = header.From;
            Geolocation = header.Geolocation;
            GeolocationError = header.GeolocationError;
            GeolocationRouting = header.GeolocationRouting;
            Hide = header.Hide;
            HistoryInfo = header.HistoryInfo;
            Identity = header.Identity;
            IdentityInfo = header.IdentityInfo;
            InfoPackage = header.InfoPackage;
            InReplyTo = header.InReplyTo;
            Join = header.Join;
            MaxBreadth = header.MaxBreadth;
            MaxForwards = header.MaxForwards;
            MimeVersion = header.MimeVersion;
            MinExpires = header.MinExpires;
            MinSe = header.MinSe;
            Organization = header.Organization;
            PaccessNetworkInfo = header.PaccessNetworkInfo;
            PanswerState = header.PanswerState;
            PassertedIdentity = header.PassertedIdentity;
            PassertedService = header.PassertedService;
            PassociatedUri = header.PassociatedUri;
            PcalledPartyId = header.PcalledPartyId;
            PchargingFunctionAddresses = header.PchargingFunctionAddresses;
            PchargingVector = header.PchargingVector;
            PdcsTracePartyId = header.PdcsTracePartyId;
            PdcsOsps = header.PdcsOsps;
            PdcsBillingInfo = header.PdcsBillingInfo;
            PdcsLaes = header.PdcsLaes;
            PdcsRedirect = header.PdcsRedirect;
            PearlyMedia = header.PearlyMedia;
            PmediaAuthorization = header.PmediaAuthorization;
            PpreferredIdentity = header.PpreferredIdentity;
            PpreferredService = header.PpreferredService;
            PprivateNetworkIndication = header.PprivateNetworkIndication;
            PprofileKey = header.PprofileKey;
            PrefusedUriList = header.PrefusedUriList;
            PservedUser = header.PservedUser;
            PuserDatabase = header.PuserDatabase;
            PvisitedNetworkId = header.PvisitedNetworkId;
            Path = header.Path;
            PermissionMissing = header.PermissionMissing;
            PolicyContact = header.PolicyContact;
            PolicyId = header.PolicyId;
            Priority = header.Priority;
            PrivAnswerMode = header.PrivAnswerMode;
            Privacy = header.Privacy;
            ProxyAuthenticate = header.ProxyAuthenticate;
            ProxyAuthorization = header.ProxyAuthorization;
            ProxyRequire = header.ProxyRequire;
            Rack = header.Rack;
            Reason = header.Reason;
            ReasonPhrase = header.ReasonPhrase;
            RecordRoute = header.RecordRoute;
            RecvInfo = header.RecvInfo;
            ReferEventsAt = header.ReferEventsAt;
            ReferStub = header.ReferStub;
            ReferTo = header.ReferTo;
            ReferredBy = header.ReferredBy;
            RejectContact = header.RejectContact;
            Replaces = header.Replaces;
            ReplyTo = header.ReplyTo;
            RequestDisposition = header.RequestDisposition;
            Require = header.Require;
            ResourcePriority = header.ResourcePriority;
            ResponseKey = header.ResponseKey;
            RetryAfter = header.RetryAfter;
            Route = header.Route;
            Rseq = header.Rseq;
            SecurityClient = header.SecurityClient;
            SecurityServer = header.SecurityServer;
            SecurityVerify = header.SecurityVerify;
            Server = header.Server;
            ServiceRoute = header.ServiceRoute;
            SessionExpires = header.SessionExpires;
            SessionId = header.SessionId;
            SipEtag = header.SipEtag;
            SipIfMatch = header.SipIfMatch;
            Subject = header.Subject;
            SubscriptionState = header.SubscriptionState;
            Supported = header.Supported;
            SuppressIfMatch = header.SuppressIfMatch;
            TargetDialog = header.TargetDialog;
            Timestamp = header.Timestamp;
            To = header.To;
            TriggerConstant = header.TriggerConstant;
            Unsupported = header.Unsupported;
            UserAgent = header.UserAgent;
            UserToUser = header.UserToUser;
            Via = header.Via;
            Warning = header.Warning;
        }

        #endregion

        #region properties

        public IMethod Method { get; }
        public IReadOnlyList<string> Accept { get; }
        public IReadOnlyList<string> AcceptContact { get; }
        public IReadOnlyList<string> AcceptEncoding { get; }
        public IReadOnlyList<string> AcceptLanguage { get; }
        public IReadOnlyList<string> AcceptResourcePriority { get; }
        public IReadOnlyList<string> AlertInfo { get; }
        public IReadOnlyList<string> Allow { get; }
        public IReadOnlyList<string> AllowEvents { get; }
        public IReadOnlyList<string> AnswerMode { get; }
        public IReadOnlyList<string> AuthenticationInfo { get; }
        public IReadOnlyList<string> Authorization { get; }
        public IReadOnlyList<string> CallId { get; }
        public IReadOnlyList<string> CallInfo { get; }
        public IReadOnlyList<string> Contact { get; }
        public IReadOnlyList<string> ContentDisposition { get; }
        public IReadOnlyList<string> ContentEncoding { get; }
        public IReadOnlyList<string> ContentLanguage { get; }
        public int ContentLength { get; }
        public string ContentType { get; }
        public IReadOnlyList<string> Cseq { get; }
        public IReadOnlyList<string> Date { get; }
        public IReadOnlyList<string> Encryption { get; }
        public IReadOnlyList<string> ErrorInfo { get; }
        public IReadOnlyList<string> Event { get; }
        public IReadOnlyList<string> Expires { get; }
        public IReadOnlyList<string> FeatureCaps { get; }
        public IReadOnlyList<string> FlowTimer { get; }
        public IReadOnlyList<string> From { get; }
        public IReadOnlyList<string> Geolocation { get; }
        public IReadOnlyList<string> GeolocationError { get; }
        public IReadOnlyList<string> GeolocationRouting { get; }
        public IReadOnlyList<string> Hide { get; }
        public IReadOnlyList<string> HistoryInfo { get; }
        public IReadOnlyList<string> Identity { get; }
        public IReadOnlyList<string> IdentityInfo { get; }
        public IReadOnlyList<string> InfoPackage { get; }
        public IReadOnlyList<string> InReplyTo { get; }
        public IReadOnlyList<string> Join { get; }
        public IReadOnlyList<string> MaxBreadth { get; }
        public IReadOnlyList<string> MaxForwards { get; }
        public IReadOnlyList<string> MimeVersion { get; }
        public IReadOnlyList<string> MinExpires { get; }
        public IReadOnlyList<string> MinSe { get; }
        public IReadOnlyList<string> Organization { get; }
        public IReadOnlyList<string> PaccessNetworkInfo { get; }
        public IReadOnlyList<string> PanswerState { get; }
        public IReadOnlyList<string> PassertedIdentity { get; }
        public IReadOnlyList<string> PassertedService { get; }
        public IReadOnlyList<string> PassociatedUri { get; }
        public IReadOnlyList<string> PcalledPartyId { get; }
        public IReadOnlyList<string> PchargingFunctionAddresses { get; }
        public IReadOnlyList<string> PchargingVector { get; }
        public IReadOnlyList<string> PdcsTracePartyId { get; }
        public IReadOnlyList<string> PdcsOsps { get; }
        public IReadOnlyList<string> PdcsBillingInfo { get; }
        public IReadOnlyList<string> PdcsLaes { get; }
        public IReadOnlyList<string> PdcsRedirect { get; }
        public IReadOnlyList<string> PearlyMedia { get; }
        public IReadOnlyList<string> PmediaAuthorization { get; }
        public IReadOnlyList<string> PpreferredIdentity { get; }
        public IReadOnlyList<string> PpreferredService { get; }
        public IReadOnlyList<string> PprivateNetworkIndication { get; }
        public IReadOnlyList<string> PprofileKey { get; }
        public IReadOnlyList<string> PrefusedUriList { get; }
        public IReadOnlyList<string> PservedUser { get; }
        public IReadOnlyList<string> PuserDatabase { get; }
        public IReadOnlyList<string> PvisitedNetworkId { get; }
        public IReadOnlyList<string> Path { get; }
        public IReadOnlyList<string> PermissionMissing { get; }
        public IReadOnlyList<string> PolicyContact { get; }
        public IReadOnlyList<string> PolicyId { get; }
        public IReadOnlyList<string> Priority { get; }
        public IReadOnlyList<string> PrivAnswerMode { get; }
        public IReadOnlyList<string> Privacy { get; }
        public IReadOnlyList<string> ProxyAuthenticate { get; }
        public IReadOnlyList<string> ProxyAuthorization { get; }
        public IReadOnlyList<string> ProxyRequire { get; }
        public IReadOnlyList<string> Rack { get; }
        public IReadOnlyList<string> Reason { get; }
        public IReadOnlyList<string> ReasonPhrase { get; }
        public IReadOnlyList<string> RecordRoute { get; }
        public IReadOnlyList<string> RecvInfo { get; }
        public IReadOnlyList<string> ReferEventsAt { get; }
        public IReadOnlyList<string> ReferStub { get; }
        public IReadOnlyList<string> ReferTo { get; }
        public IReadOnlyList<string> ReferredBy { get; }
        public IReadOnlyList<string> RejectContact { get; }
        public IReadOnlyList<string> Replaces { get; }
        public IReadOnlyList<string> ReplyTo { get; }
        public IReadOnlyList<string> RequestDisposition { get; }
        public IReadOnlyList<string> Require { get; }
        public IReadOnlyList<string> ResourcePriority { get; }
        public IReadOnlyList<string> ResponseKey { get; }
        public IReadOnlyList<string> RetryAfter { get; }
        public IReadOnlyList<string> Route { get; }
        public IReadOnlyList<string> Rseq { get; }
        public IReadOnlyList<string> SecurityClient { get; }
        public IReadOnlyList<string> SecurityServer { get; }
        public IReadOnlyList<string> SecurityVerify { get; }
        public IReadOnlyList<string> Server { get; }
        public IReadOnlyList<string> ServiceRoute { get; }
        public IReadOnlyList<string> SessionExpires { get; }
        public IReadOnlyList<string> SessionId { get; }
        public IReadOnlyList<string> SipEtag { get; }
        public IReadOnlyList<string> SipIfMatch { get; }
        public IReadOnlyList<string> Subject { get; }
        public IReadOnlyList<string> SubscriptionState { get; }
        public IReadOnlyList<string> Supported { get; }
        public IReadOnlyList<string> SuppressIfMatch { get; }
        public IReadOnlyList<string> TargetDialog { get; }
        public IReadOnlyList<string> Timestamp { get; }
        public IReadOnlyList<string> To { get; }
        public IReadOnlyList<string> TriggerConstant { get; }
        public IReadOnlyList<string> Unsupported { get; }
        public IReadOnlyList<string> UserAgent { get; }
        public IReadOnlyList<string> UserToUser { get; }
        public IReadOnlyList<string> Via { get; }
        public IReadOnlyList<string> Warning { get; }
        public IReadOnlyList<string> WwwAuthenticate { get; }

        #endregion

        #region public functions

        public void AddTo(MessageBuilder messageBuilder)
        {
            Method.AddTo(messageBuilder);
            AddToMessage(messageBuilder, HeaderFieldType.From, From);
            AddToMessage(messageBuilder, HeaderFieldType.To, To);
            AddToMessage(messageBuilder, HeaderFieldType.Via, Via);
            AddToMessage(messageBuilder, HeaderFieldType.MaxForwards, MaxForwards);
            AddToMessage(messageBuilder, HeaderFieldType.CallId, CallId);
            AddToMessage(messageBuilder, HeaderFieldType.Cseq, Cseq);
            AddToMessage(messageBuilder, HeaderFieldType.Contact, Contact);
            AddToMessage(messageBuilder, HeaderFieldType.Route, Route);
            AddToMessage(messageBuilder, HeaderFieldType.RecordRoute, RecordRoute);
            AddToMessage(messageBuilder, HeaderFieldType.Accept, Accept);
            AddToMessage(messageBuilder, HeaderFieldType.AcceptContact, AcceptContact);
            AddToMessage(messageBuilder, HeaderFieldType.AcceptEncoding, AcceptEncoding);
            AddToMessage(messageBuilder, HeaderFieldType.AcceptLanguage, AcceptLanguage);
            AddToMessage(messageBuilder, HeaderFieldType.AcceptResourcePriority, AcceptResourcePriority);
            AddToMessage(messageBuilder, HeaderFieldType.AlertInfo, AlertInfo);
            AddToMessage(messageBuilder, HeaderFieldType.Allow, Allow);
            AddToMessage(messageBuilder, HeaderFieldType.AllowEvents, AllowEvents);
            AddToMessage(messageBuilder, HeaderFieldType.AnswerMode, AnswerMode);
            AddToMessage(messageBuilder, HeaderFieldType.AuthenticationInfo, AuthenticationInfo);
            AddToMessage(messageBuilder, HeaderFieldType.Authorization, Authorization);
            AddToMessage(messageBuilder, HeaderFieldType.CallInfo, CallInfo);
            AddToMessage(messageBuilder, HeaderFieldType.ContentDisposition, ContentDisposition);
            AddToMessage(messageBuilder, HeaderFieldType.ContentEncoding, ContentEncoding);
            AddToMessage(messageBuilder, HeaderFieldType.ContentLanguage, ContentLanguage);
            AddToMessage(messageBuilder, HeaderFieldType.Date, Date);
            AddToMessage(messageBuilder, HeaderFieldType.Encryption, Encryption);
            AddToMessage(messageBuilder, HeaderFieldType.ErrorInfo, ErrorInfo);
            AddToMessage(messageBuilder, HeaderFieldType.Event, Event);
            AddToMessage(messageBuilder, HeaderFieldType.Expires, Expires);
            AddToMessage(messageBuilder, HeaderFieldType.FeatureCaps, FeatureCaps);
            AddToMessage(messageBuilder, HeaderFieldType.FlowTimer, FlowTimer);
            AddToMessage(messageBuilder, HeaderFieldType.Geolocation, Geolocation);
            AddToMessage(messageBuilder, HeaderFieldType.GeolocationError, GeolocationError);
            AddToMessage(messageBuilder, HeaderFieldType.GeolocationRouting, GeolocationRouting);
            AddToMessage(messageBuilder, HeaderFieldType.Hide, Hide);
            AddToMessage(messageBuilder, HeaderFieldType.HistoryInfo, HistoryInfo);
            AddToMessage(messageBuilder, HeaderFieldType.Identity, Identity);
            AddToMessage(messageBuilder, HeaderFieldType.IdentityInfo, IdentityInfo);
            AddToMessage(messageBuilder, HeaderFieldType.InfoPackage, InfoPackage);
            AddToMessage(messageBuilder, HeaderFieldType.InReplyTo, InReplyTo);
            AddToMessage(messageBuilder, HeaderFieldType.Join, Join);
            AddToMessage(messageBuilder, HeaderFieldType.MaxBreadth, MaxBreadth);
            AddToMessage(messageBuilder, HeaderFieldType.MimeVersion, MimeVersion);
            AddToMessage(messageBuilder, HeaderFieldType.MinExpires, MinExpires);
            AddToMessage(messageBuilder, HeaderFieldType.MinSe, MinSe);
            AddToMessage(messageBuilder, HeaderFieldType.Organization, Organization);
            AddToMessage(messageBuilder, HeaderFieldType.PaccessNetworkInfo, PaccessNetworkInfo);
            AddToMessage(messageBuilder, HeaderFieldType.PanswerState, PanswerState);
            AddToMessage(messageBuilder, HeaderFieldType.PassertedIdentity, PassertedIdentity);
            AddToMessage(messageBuilder, HeaderFieldType.PassertedService, PassertedService);
            AddToMessage(messageBuilder, HeaderFieldType.PassociatedUri, PassociatedUri);
            AddToMessage(messageBuilder, HeaderFieldType.PcalledPartyId, PcalledPartyId);
            AddToMessage(messageBuilder, HeaderFieldType.PchargingFunctionAddresses, PchargingFunctionAddresses);
            AddToMessage(messageBuilder, HeaderFieldType.PchargingVector, PchargingVector);
            AddToMessage(messageBuilder, HeaderFieldType.PdcsTracePartyId, PdcsTracePartyId);
            AddToMessage(messageBuilder, HeaderFieldType.PdcsOsps, PdcsOsps);
            AddToMessage(messageBuilder, HeaderFieldType.PdcsBillingInfo, PdcsBillingInfo);
            AddToMessage(messageBuilder, HeaderFieldType.PdcsLaes, PdcsLaes);
            AddToMessage(messageBuilder, HeaderFieldType.PdcsRedirect, PdcsRedirect);
            AddToMessage(messageBuilder, HeaderFieldType.PearlyMedia, PearlyMedia);
            AddToMessage(messageBuilder, HeaderFieldType.PmediaAuthorization, PmediaAuthorization);
            AddToMessage(messageBuilder, HeaderFieldType.PpreferredIdentity, PpreferredIdentity);
            AddToMessage(messageBuilder, HeaderFieldType.PpreferredService, PpreferredService);
            AddToMessage(messageBuilder, HeaderFieldType.PprivateNetworkIndication, PprivateNetworkIndication);
            AddToMessage(messageBuilder, HeaderFieldType.PprofileKey, PprofileKey);
            AddToMessage(messageBuilder, HeaderFieldType.PrefusedUriList, PrefusedUriList);
            AddToMessage(messageBuilder, HeaderFieldType.PservedUser, PservedUser);
            AddToMessage(messageBuilder, HeaderFieldType.PuserDatabase, PuserDatabase);
            AddToMessage(messageBuilder, HeaderFieldType.PvisitedNetworkId, PvisitedNetworkId);
            AddToMessage(messageBuilder, HeaderFieldType.Path, Path);
            AddToMessage(messageBuilder, HeaderFieldType.PermissionMissing, PermissionMissing);
            AddToMessage(messageBuilder, HeaderFieldType.PolicyContact, PolicyContact);
            AddToMessage(messageBuilder, HeaderFieldType.PolicyId, PolicyId);
            AddToMessage(messageBuilder, HeaderFieldType.Priority, Priority);
            AddToMessage(messageBuilder, HeaderFieldType.PrivAnswerMode, PrivAnswerMode);
            AddToMessage(messageBuilder, HeaderFieldType.Privacy, Privacy);
            AddToMessage(messageBuilder, HeaderFieldType.ProxyAuthenticate, ProxyAuthenticate);
            AddToMessage(messageBuilder, HeaderFieldType.ProxyAuthorization, ProxyAuthorization);
            AddToMessage(messageBuilder, HeaderFieldType.ProxyRequire, ProxyRequire);
            AddToMessage(messageBuilder, HeaderFieldType.Rack, Rack);
            AddToMessage(messageBuilder, HeaderFieldType.Reason, Reason);
            AddToMessage(messageBuilder, HeaderFieldType.ReasonPhrase, ReasonPhrase);
            AddToMessage(messageBuilder, HeaderFieldType.RecvInfo, RecvInfo);
            AddToMessage(messageBuilder, HeaderFieldType.ReferEventsAt, ReferEventsAt);
            AddToMessage(messageBuilder, HeaderFieldType.ReferStub, ReferStub);
            AddToMessage(messageBuilder, HeaderFieldType.ReferTo, ReferTo);
            AddToMessage(messageBuilder, HeaderFieldType.ReferredBy, ReferredBy);
            AddToMessage(messageBuilder, HeaderFieldType.RejectContact, RejectContact);
            AddToMessage(messageBuilder, HeaderFieldType.Replaces, Replaces);
            AddToMessage(messageBuilder, HeaderFieldType.ReplyTo, ReplyTo);
            AddToMessage(messageBuilder, HeaderFieldType.RequestDisposition, RequestDisposition);
            AddToMessage(messageBuilder, HeaderFieldType.Require, Require);
            AddToMessage(messageBuilder, HeaderFieldType.ResourcePriority, ResourcePriority);
            AddToMessage(messageBuilder, HeaderFieldType.ResponseKey, ResponseKey);
            AddToMessage(messageBuilder, HeaderFieldType.RetryAfter, RetryAfter);
            AddToMessage(messageBuilder, HeaderFieldType.Rseq, Rseq);
            AddToMessage(messageBuilder, HeaderFieldType.SecurityClient, SecurityClient);
            AddToMessage(messageBuilder, HeaderFieldType.SecurityServer, SecurityServer);
            AddToMessage(messageBuilder, HeaderFieldType.SecurityVerify, SecurityVerify);
            AddToMessage(messageBuilder, HeaderFieldType.Server, Server);
            AddToMessage(messageBuilder, HeaderFieldType.ServiceRoute, ServiceRoute);
            AddToMessage(messageBuilder, HeaderFieldType.SessionExpires, SessionExpires);
            AddToMessage(messageBuilder, HeaderFieldType.SessionId, SessionId);
            AddToMessage(messageBuilder, HeaderFieldType.SipEtag, SipEtag);
            AddToMessage(messageBuilder, HeaderFieldType.SipIfMatch, SipIfMatch);
            AddToMessage(messageBuilder, HeaderFieldType.Subject, Subject);
            AddToMessage(messageBuilder, HeaderFieldType.SubscriptionState, SubscriptionState);
            AddToMessage(messageBuilder, HeaderFieldType.Supported, Supported);
            AddToMessage(messageBuilder, HeaderFieldType.SuppressIfMatch, SuppressIfMatch);
            AddToMessage(messageBuilder, HeaderFieldType.TargetDialog, TargetDialog);
            AddToMessage(messageBuilder, HeaderFieldType.Timestamp, Timestamp);
            AddToMessage(messageBuilder, HeaderFieldType.TriggerConstant, TriggerConstant);
            AddToMessage(messageBuilder, HeaderFieldType.Unsupported, Unsupported);
            AddToMessage(messageBuilder, HeaderFieldType.UserAgent, UserAgent);
            AddToMessage(messageBuilder, HeaderFieldType.UserToUser, UserToUser);
            AddToMessage(messageBuilder, HeaderFieldType.Warning, Warning);
            AddToMessage(messageBuilder, HeaderFieldType.WwwAuthenticate, WwwAuthenticate);
            AddToMessage(messageBuilder, HeaderFieldType.ContentType, ContentType);
            AddToMessage(messageBuilder, HeaderFieldType.ContentLength, ContentLength);
        }

        #endregion

        #region private functions

        private static void AddToMessage(MessageBuilder messageBuilder, HeaderFieldType headerFieldType, IReadOnlyList<string> values)
        {
            if (values == null || values.Count == 0)
                return;

            messageBuilder.AddHeaderLineWithMultipleValues(HeaderFieldTypeUtils.ToFriendlyString(headerFieldType), values);
        }

        private static void AddToMessage(MessageBuilder messageBuilder, HeaderFieldType headerFieldType, string value)
        {
            if (value == null)
                return;

            messageBuilder.AddLineFormat("{0}: {1}", HeaderFieldTypeUtils.ToFriendlyString(headerFieldType), value);
        }

        private static void AddToMessage(MessageBuilder messageBuilder, HeaderFieldType headerFieldType, int value)
        {
            messageBuilder.AddLineFormat("{0}: {1}", HeaderFieldTypeUtils.ToFriendlyString(headerFieldType), value.ToString());
        }

        #endregion
    }
}
