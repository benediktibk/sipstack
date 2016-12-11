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
            header.Method = requestLineResult.Result;
            
            foreach (var field in fieldsCombined)
            {
                if (field.Values.Count > 1 && !field.Name.CanHaveMultipleValues)
                    return new ParseResult<Header>($"field of type {field.Name} has multiple values");

                var headerResult = ParseField(header, field);

                if (headerResult.IsError)
                    return headerResult.ToParseResult<Header>();

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
                case HeaderFieldType.Accept:
                    header.Accept = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.AcceptContact:
                    header.AcceptContact = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.AcceptEncoding:
                    header.AcceptEncoding = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.AcceptLanguage:
                    header.AcceptLanguage = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.AcceptResourcePriority:
                    header.AcceptResourcePriority = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.AlertInfo:
                    header.AlertInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Allow:
                    header.Allow = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.AllowEvents:
                    header.AllowEvents = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.AnswerMode:
                    header.AnswerMode = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.AuthenticationInfo:
                    header.AuthenticationInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Authorization:
                    header.Authorization = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.CallId:
                    header.CallId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.CallInfo:
                    header.CallInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Contact:
                    header.Contact = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ContentDisposition:
                    header.ContentDisposition = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ContentEncoding:
                    header.ContentEncoding = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ContentLanguage:
                    header.ContentLanguage = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ContentLength:
                    var contentLength = ParseSingleInt(field.Values);

                    if (contentLength.IsError)
                        return contentLength.ToParseResult<HeaderDto>();

                    header.ContentLength = contentLength.Result;
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ContentType:
                    var contentType = ParseSingleString(field.Values);

                    if (contentType.IsError)
                        return contentType.ToParseResult<HeaderDto>();

                    header.ContentType = contentType.Result;
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Cseq:
                    header.Cseq = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Date:
                    header.Date = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Encryption:
                    header.Encryption = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ErrorInfo:
                    header.ErrorInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Event:
                    header.Event = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Expires:
                    header.Expires = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.FeatureCaps:
                    header.FeatureCaps = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.FlowTimer:
                    header.FlowTimer = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.From:
                    header.From = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Geolocation:
                    header.Geolocation = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.GeolocationError:
                    header.GeolocationError = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.GeolocationRouting:
                    header.GeolocationRouting = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Hide:
                    header.Hide = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.HistoryInfo:
                    header.HistoryInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Identity:
                    header.Identity = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.IdentityInfo:
                    header.IdentityInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.InfoPackage:
                    header.InfoPackage = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.InReplyTo:
                    header.InReplyTo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Join:
                    header.Join = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.MaxBreadth:
                    header.MaxBreadth = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.MaxForwards:
                    header.MaxForwards = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.MimeVersion:
                    header.MimeVersion = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.MinExpires:
                    header.MinExpires = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.MinSe:
                    header.MinSe = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Organization:
                    header.Organization = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PaccessNetworkInfo:
                    header.PaccessNetworkInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PanswerState:
                    header.PanswerState = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PassertedIdentity:
                    header.PassertedIdentity = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PassertedService:
                    header.PassertedService = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PassociatedUri:
                    header.PassociatedUri = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PcalledPartyId:
                    header.PcalledPartyId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PchargingFunctionAddresses:
                    header.PchargingFunctionAddresses = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PchargingVector:
                    header.PchargingVector = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PdcsTracePartyId:
                    header.PdcsTracePartyId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PdcsOsps:
                    header.PdcsOsps = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PdcsBillingInfo:
                    header.PdcsBillingInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PdcsLaes:
                    header.PdcsLaes = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PdcsRedirect:
                    header.PdcsRedirect = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PearlyMedia:
                    header.PearlyMedia = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PmediaAuthorization:
                    header.PmediaAuthorization = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PpreferredIdentity:
                    header.PpreferredIdentity = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PpreferredService:
                    header.PpreferredService = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PprivateNetworkIndication:
                    header.PprivateNetworkIndication = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PprofileKey:
                    header.PprofileKey = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PrefusedUriList:
                    header.PrefusedUriList = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PservedUser:
                    header.PservedUser = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PuserDatabase:
                    header.PuserDatabase = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PvisitedNetworkId:
                    header.PvisitedNetworkId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Path:
                    header.Path = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PermissionMissing:
                    header.PermissionMissing = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PolicyContact:
                    header.PolicyContact = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PolicyId:
                    header.PolicyId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Priority:
                    header.Priority = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.PrivAnswerMode:
                    header.PrivAnswerMode = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Privacy:
                    header.Privacy = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ProxyAuthenticate:
                    header.ProxyAuthenticate = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ProxyAuthorization:
                    header.ProxyAuthorization = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ProxyRequire:
                    header.ProxyRequire = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Rack:
                    header.Rack = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Reason:
                    header.Reason = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ReasonPhrase:
                    header.ReasonPhrase = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.RecordRoute:
                    header.RecordRoute = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.RecvInfo:
                    header.RecvInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ReferEventsAt:
                    header.ReferEventsAt = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ReferStub:
                    header.ReferStub = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ReferTo:
                    header.ReferTo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ReferredBy:
                    header.ReferredBy = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.RejectContact:
                    header.RejectContact = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Replaces:
                    header.Replaces = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ReplyTo:
                    header.ReplyTo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.RequestDisposition:
                    header.RequestDisposition = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Require:
                    header.Require = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ResourcePriority:
                    header.ResourcePriority = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ResponseKey:
                    header.ResponseKey = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.RetryAfter:
                    header.RetryAfter = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Route:
                    header.Route = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Rseq:
                    header.Rseq = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.SecurityClient:
                    header.SecurityClient = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.SecurityServer:
                    header.SecurityServer = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.SecurityVerify:
                    header.SecurityVerify = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Server:
                    header.Server = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.ServiceRoute:
                    header.ServiceRoute = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.SessionExpires:
                    header.SessionExpires = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.SessionId:
                    header.SessionId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.SipEtag:
                    header.SipEtag = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.SipIfMatch:
                    header.SipIfMatch = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Subject:
                    header.Subject = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.SubscriptionState:
                    header.SubscriptionState = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Supported:
                    header.Supported = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.SuppressIfMatch:
                    header.SuppressIfMatch = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.TargetDialog:
                    header.TargetDialog = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Timestamp:
                    header.Timestamp = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.To:
                    header.To = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.TriggerConstant:
                    header.TriggerConstant = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Unsupported:
                    header.Unsupported = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.UserAgent:
                    header.UserAgent = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.UserToUser:
                    header.UserToUser = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Via:
                    header.Via = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.Warning:
                    header.Warning = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case HeaderFieldType.WwwAuthenticate:
                    header.WwwAuthenticate = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                default:
                    throw new NotImplementedException($"header field type {field.Name} is not implemented");
            }
        }

        public static ParseResult<int> ParseSingleInt(IReadOnlyList<string> values)
        {
            if (values.Count < 1)
                return new ParseResult<int>("the value for a header field is missing");

            if (values.Count > 1)
                return new ParseResult<int>("there is more than one value for a header field, which should have only one value");
            
            int value;

            if (!int.TryParse(values[0], out value))
                return new ParseResult<int>($"could not parse the string {values[0]} as int");

            return new ParseResult<int>(value);
        }

        public static ParseResult<string> ParseSingleString(IReadOnlyList<string> values)
        {
            if (values.Count < 1)
                return new ParseResult<string>("the value for a header field is missing");

            if (values.Count > 1)
                return new ParseResult<string>("there is more than one value for a header field, which should have only one value");

            return new ParseResult<string>(values[0]);
        }
    }
}
