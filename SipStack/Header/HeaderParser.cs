using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Header
{
    public class HeaderParser
    {
        private readonly RequestLineParser _requestLineParser;
        private readonly HeaderFieldParser _headerFieldParser;

        public HeaderParser(RequestLineParser requestLineParser, HeaderFieldParser headerFieldParser)
        {
            _requestLineParser = requestLineParser;
            _headerFieldParser = headerFieldParser;
        }

        public ParseResult<Header> Parse(IReadOnlyList<string> lines, int headerEnd)
        {
            var requestLineResult = _requestLineParser.Parse(lines[0]);
            if (requestLineResult.IsError)
                return requestLineResult.ToParseResult<Header>();

            var fieldsResult = ParseHeaderFields(lines, headerEnd);

            if (fieldsResult.IsError)
                return fieldsResult.ToParseResult<Header>();

            var fields = fieldsResult.Result;
            var fieldsCombined = CombineHeaderFieldsWithSameType(fields);

            var header = new HeaderDto();
            header.Method = requestLineResult.Result.Type;
            
            foreach (var field in fieldsCombined)
            {
                if (field.Values.Count > 1 && !field.Name.CanHaveMultipleValues)
                    return new ParseResult<Header>($"field of type {field.Name} has multiple values");

                var headerResult = ParseField(header, field);

                if (headerResult.IsError)
                    return headerResult.ToResult<Header>();

                header = headerResult.Result;
            }

            return new ParseResult<Header>(new Header(header));
        }

        private ParseResult<List<HeaderField>> ParseHeaderFields(IReadOnlyList<string> lines, int headerEnd)
        {
            var fields = new List<HeaderField>();
            for (var i = 1; i < headerEnd; ++i)
            {
                var end = 0;
                var headerFieldResult = _headerFieldParser.Parse(lines, i, out end);

                if (headerFieldResult.IsError)
                    return headerFieldResult.ToParseResult<List<HeaderField>>();

                var headerField = headerFieldResult.Result;
                fields.Add(headerField);

                i = end;
            }

            return new ParseResult<List<HeaderField>>(fields);
        }

        private static List<HeaderField> CombineHeaderFieldsWithSameType(List<HeaderField> fields)
        {
            var fieldsByType = new Dictionary<HeaderFieldName, List<string>>();

            foreach (var field in fields)
            {
                var type = field.Name;
                List<string> values;

                if (!fieldsByType.TryGetValue(type, out values))
                {
                    values = new List<string>(1);
                    fieldsByType.Add(type, values);
                }


                values.AddRange(field.Values);
            }

            return fieldsByType.Select(x => new HeaderField(x.Key, x.Value)).ToList();
        }

        private static ParseResult<HeaderDto> ParseField(HeaderDto header, HeaderField field)
        {
            if (field.Name.IsCustomField)
            {
                header.CustomHeaders.Add(field);
                return new ParseResult<HeaderDto>(header);
            }

            switch(field.Name.Type)
            {
                case HeaderFieldType.Accept: header.Accept = field.Values.ToList(); break;
                case HeaderFieldType.AcceptContact: header.AcceptContact = field.Values.ToList(); break;
                case HeaderFieldType.AcceptEncoding: header.AcceptEncoding = field.Values.ToList(); break;
                case HeaderFieldType.AcceptLanguage: header.AcceptLanguage = field.Values.ToList(); break;
                case HeaderFieldType.AcceptResourcePriority: header.AcceptResourcePriority = field.Values.ToList(); break;
                case HeaderFieldType.AlertInfo: header.AlertInfo = field.Values.ToList(); break;
                case HeaderFieldType.Allow: header.Allow = field.Values.ToList(); break;
                case HeaderFieldType.AllowEvents: header.AllowEvents = field.Values.ToList(); break;
                case HeaderFieldType.AnswerMode: header.AnswerMode = field.Values.ToList(); break;
                case HeaderFieldType.AuthenticationInfo: header.AuthenticationInfo = field.Values.ToList(); break;
                case HeaderFieldType.Authorization: header.Authorization = field.Values.ToList(); break;
                case HeaderFieldType.CallId: header.CallId = field.Values.ToList(); break;
                case HeaderFieldType.CallInfo: header.CallInfo = field.Values.ToList(); break;
                case HeaderFieldType.Contact: header.Contact = field.Values.ToList(); break;
                case HeaderFieldType.ContentDisposition: header.ContentDisposition = field.Values.ToList(); break;
                case HeaderFieldType.ContentEncoding: header.ContentEncoding = field.Values.ToList(); break;
                case HeaderFieldType.ContentLanguage: header.ContentLanguage = field.Values.ToList(); break;
                case HeaderFieldType.ContentLength: header.ContentLength = field.Values.ToList(); break;
                case HeaderFieldType.ContentType: header.ContentType = field.Values.ToList(); break;
                case HeaderFieldType.Cseq: header.Cseq = field.Values.ToList(); break;
                case HeaderFieldType.Date: header.Date = field.Values.ToList(); break;
                case HeaderFieldType.Encryption: header.Encryption = field.Values.ToList(); break;
                case HeaderFieldType.ErrorInfo: header.ErrorInfo = field.Values.ToList(); break;
                case HeaderFieldType.Event: header.Event = field.Values.ToList(); break;
                case HeaderFieldType.Expires: header.Expires = field.Values.ToList(); break;
                case HeaderFieldType.FeatureCaps: header.FeatureCaps = field.Values.ToList(); break;
                case HeaderFieldType.FlowTimer: header.FlowTimer = field.Values.ToList(); break;
                case HeaderFieldType.From: header.From = field.Values.ToList(); break;
                case HeaderFieldType.Geolocation: header.Geolocation = field.Values.ToList(); break;
                case HeaderFieldType.GeolocationError: header.GeolocationError = field.Values.ToList(); break;
                case HeaderFieldType.GeolocationRouting: header.GeolocationRouting = field.Values.ToList(); break;
                case HeaderFieldType.Hide: header.Hide = field.Values.ToList(); break;
                case HeaderFieldType.HistoryInfo: header.HistoryInfo = field.Values.ToList(); break;
                case HeaderFieldType.Identity: header.Identity = field.Values.ToList(); break;
                case HeaderFieldType.IdentityInfo: header.IdentityInfo = field.Values.ToList(); break;
                case HeaderFieldType.InfoPackage: header.InfoPackage = field.Values.ToList(); break;
                case HeaderFieldType.InReplyTo: header.InReplyTo = field.Values.ToList(); break;
                case HeaderFieldType.Join: header.Join = field.Values.ToList(); break;
                case HeaderFieldType.MaxBreadth: header.MaxBreadth = field.Values.ToList(); break;
                case HeaderFieldType.MaxForwards: header.MaxForwards = field.Values.ToList(); break;
                case HeaderFieldType.MimeVersion: header.MimeVersion = field.Values.ToList(); break;
                case HeaderFieldType.MinExpires: header.MinExpires = field.Values.ToList(); break;
                case HeaderFieldType.MinSe: header.MinSe = field.Values.ToList(); break;
                case HeaderFieldType.Organization: header.Organization = field.Values.ToList(); break;
                case HeaderFieldType.PaccessNetworkInfo: header.PaccessNetworkInfo = field.Values.ToList(); break;
                case HeaderFieldType.PanswerState: header.PanswerState = field.Values.ToList(); break;
                case HeaderFieldType.PassertedIdentity: header.PassertedIdentity = field.Values.ToList(); break;
                case HeaderFieldType.PassertedService: header.PassertedService = field.Values.ToList(); break;
                case HeaderFieldType.PassociatedUri: header.PassociatedUri = field.Values.ToList(); break;
                case HeaderFieldType.PcalledPartyId: header.PcalledPartyId = field.Values.ToList(); break;
                case HeaderFieldType.PchargingFunctionAddresses: header.PchargingFunctionAddresses = field.Values.ToList(); break;
                case HeaderFieldType.PchargingVector: header.PchargingVector = field.Values.ToList(); break;
                case HeaderFieldType.PdcsTracePartyId: header.PdcsTracePartyId = field.Values.ToList(); break;
                case HeaderFieldType.PdcsOsps: header.PdcsOsps = field.Values.ToList(); break;
                case HeaderFieldType.PdcsBillingInfo: header.PdcsBillingInfo = field.Values.ToList(); break;
                case HeaderFieldType.PdcsLaes: header.PdcsLaes = field.Values.ToList(); break;
                case HeaderFieldType.PdcsRedirect: header.PdcsRedirect = field.Values.ToList(); break;
                case HeaderFieldType.PearlyMedia: header.PearlyMedia = field.Values.ToList(); break;
                case HeaderFieldType.PmediaAuthorization: header.PmediaAuthorization = field.Values.ToList(); break;
                case HeaderFieldType.PpreferredIdentity: header.PpreferredIdentity = field.Values.ToList(); break;
                case HeaderFieldType.PpreferredService: header.PpreferredService = field.Values.ToList(); break;
                case HeaderFieldType.PprivateNetworkIndication: header.PprivateNetworkIndication = field.Values.ToList(); break;
                case HeaderFieldType.PprofileKey: header.PprofileKey = field.Values.ToList(); break;
                case HeaderFieldType.PrefusedUriList: header.PrefusedUriList = field.Values.ToList(); break;
                case HeaderFieldType.PservedUser: header.PservedUser = field.Values.ToList(); break;
                case HeaderFieldType.PuserDatabase: header.PuserDatabase = field.Values.ToList(); break;
                case HeaderFieldType.PvisitedNetworkId: header.PvisitedNetworkId = field.Values.ToList(); break;
                case HeaderFieldType.Path: header.Path = field.Values.ToList(); break;
                case HeaderFieldType.PermissionMissing: header.PermissionMissing = field.Values.ToList(); break;
                case HeaderFieldType.PolicyContact: header.PolicyContact = field.Values.ToList(); break;
                case HeaderFieldType.PolicyId: header.PolicyId = field.Values.ToList(); break;
                case HeaderFieldType.Priority: header.Priority = field.Values.ToList(); break;
                case HeaderFieldType.PrivAnswerMode: header.PrivAnswerMode = field.Values.ToList(); break;
                case HeaderFieldType.Privacy: header.Privacy = field.Values.ToList(); break;
                case HeaderFieldType.ProxyAuthenticate: header.ProxyAuthenticate = field.Values.ToList(); break;
                case HeaderFieldType.ProxyAuthorization: header.ProxyAuthorization = field.Values.ToList(); break;
                case HeaderFieldType.ProxyRequire: header.ProxyRequire = field.Values.ToList(); break;
                case HeaderFieldType.Rack: header.Rack = field.Values.ToList(); break;
                case HeaderFieldType.Reason: header.Reason = field.Values.ToList(); break;
                case HeaderFieldType.ReasonPhrase: header.ReasonPhrase = field.Values.ToList(); break;
                case HeaderFieldType.RecordRoute: header.RecordRoute = field.Values.ToList(); break;
                case HeaderFieldType.RecvInfo: header.RecvInfo = field.Values.ToList(); break;
                case HeaderFieldType.ReferEventsAt: header.ReferEventsAt = field.Values.ToList(); break;
                case HeaderFieldType.ReferStub: header.ReferStub = field.Values.ToList(); break;
                case HeaderFieldType.ReferTo: header.ReferTo = field.Values.ToList(); break;
                case HeaderFieldType.ReferredBy: header.ReferredBy = field.Values.ToList(); break;
                case HeaderFieldType.RejectContact: header.RejectContact = field.Values.ToList(); break;
                case HeaderFieldType.Replaces: header.Replaces = field.Values.ToList(); break;
                case HeaderFieldType.ReplyTo: header.ReplyTo = field.Values.ToList(); break;
                case HeaderFieldType.RequestDisposition: header.RequestDisposition = field.Values.ToList(); break;
                case HeaderFieldType.Require: header.Require = field.Values.ToList(); break;
                case HeaderFieldType.ResourcePriority: header.ResourcePriority = field.Values.ToList(); break;
                case HeaderFieldType.ResponseKey: header.ResponseKey = field.Values.ToList(); break;
                case HeaderFieldType.RetryAfter: header.RetryAfter = field.Values.ToList(); break;
                case HeaderFieldType.Route: header.Route = field.Values.ToList(); break;
                case HeaderFieldType.Rseq: header.Rseq = field.Values.ToList(); break;
                case HeaderFieldType.SecurityClient: header.SecurityClient = field.Values.ToList(); break;
                case HeaderFieldType.SecurityServer: header.SecurityServer = field.Values.ToList(); break;
                case HeaderFieldType.SecurityVerify: header.SecurityVerify = field.Values.ToList(); break;
                case HeaderFieldType.Server: header.Server = field.Values.ToList(); break;
                case HeaderFieldType.ServiceRoute: header.ServiceRoute = field.Values.ToList(); break;
                case HeaderFieldType.SessionExpires: header.SessionExpires = field.Values.ToList(); break;
                case HeaderFieldType.SessionId: header.SessionId = field.Values.ToList(); break;
                case HeaderFieldType.SipEtag: header.SipEtag = field.Values.ToList(); break;
                case HeaderFieldType.SipIfMatch: header.SipIfMatch = field.Values.ToList(); break;
                case HeaderFieldType.Subject: header.Subject = field.Values.ToList(); break;
                case HeaderFieldType.SubscriptionState: header.SubscriptionState = field.Values.ToList(); break;
                case HeaderFieldType.Supported: header.Supported = field.Values.ToList(); break;
                case HeaderFieldType.SuppressIfMatch: header.SuppressIfMatch = field.Values.ToList(); break;
                case HeaderFieldType.TargetDialog: header.TargetDialog = field.Values.ToList(); break;
                case HeaderFieldType.Timestamp: header.Timestamp = field.Values.ToList(); break;
                case HeaderFieldType.To: header.To = field.Values.ToList(); break;
                case HeaderFieldType.TriggerConstant: header.TriggerConstant = field.Values.ToList(); break;
                case HeaderFieldType.Unsupported: header.Unsupported = field.Values.ToList(); break;
                case HeaderFieldType.UserAgent: header.UserAgent = field.Values.ToList(); break;
                case HeaderFieldType.UserToUser: header.UserToUser = field.Values.ToList(); break;
                case HeaderFieldType.Via: header.Via = field.Values.ToList(); break;
                case HeaderFieldType.Warning: header.Warning = field.Values.ToList(); break;
                case HeaderFieldType.WwwAuthenticate: header.WwwAuthenticate = field.Values.ToList(); break;
            }
        }
    }
}
