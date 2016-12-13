using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SipStack.Header
{
    public class HeaderParser
    {
        #region variables

        private readonly RequestLineParser _requestLineParser;
        private readonly FieldParser _headerFieldParser;

        #endregion

        #region constructors

        public HeaderParser(RequestLineParser requestLineParser, FieldParser headerFieldParser)
        {
            _requestLineParser = requestLineParser;
            _headerFieldParser = headerFieldParser;
        }

        #endregion

        #region public functions

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

        #endregion

        #region private static functions

        private static List<HeaderField> CombineHeaderFieldsWithSameType(List<HeaderField> fields)
        {
            var fieldsByType = new Dictionary<FieldName, List<string>>();

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

            switch (field.Name.Type)
            {
                case FieldType.Accept:
                    header.Accept = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.AcceptContact:
                    header.AcceptContact = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.AcceptEncoding:
                    header.AcceptEncoding = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.AcceptLanguage:
                    header.AcceptLanguage = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.AcceptResourcePriority:
                    header.AcceptResourcePriority = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.AlertInfo:
                    header.AlertInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Allow:
                    header.Allow = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.AllowEvents:
                    header.AllowEvents = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.AnswerMode:
                    header.AnswerMode = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.AuthenticationInfo:
                    header.AuthenticationInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Authorization:
                    header.Authorization = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.CallId:
                    header.CallId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.CallInfo:
                    header.CallInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Contact:
                    header.Contact = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ContentDisposition:
                    header.ContentDisposition = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ContentEncoding:
                    header.ContentEncoding = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ContentLanguage:
                    header.ContentLanguage = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ContentLength:
                    var contentLength = ParseSingleValue(field.Values, ConvertStringToInt);

                    if (contentLength.IsError)
                        return contentLength.ToParseResult<HeaderDto>();

                    header.ContentLength = contentLength.Result;
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ContentType:
                    var contentType = ParseSingleValue(field.Values, (x) => { return new ParseResult<string>(x, true); });

                    if (contentType.IsError)
                        return contentType.ToParseResult<HeaderDto>();

                    header.ContentType = contentType.Result;
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Cseq:
                    header.Cseq = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Date:
                    header.Date = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Encryption:
                    header.Encryption = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ErrorInfo:
                    header.ErrorInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Event:
                    header.Event = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Expires:
                    header.Expires = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.FeatureCaps:
                    header.FeatureCaps = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.FlowTimer:
                    header.FlowTimer = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.From:
                    header.From = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Geolocation:
                    header.Geolocation = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.GeolocationError:
                    header.GeolocationError = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.GeolocationRouting:
                    header.GeolocationRouting = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Hide:
                    header.Hide = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.HistoryInfo:
                    header.HistoryInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Identity:
                    header.Identity = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.IdentityInfo:
                    header.IdentityInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.InfoPackage:
                    header.InfoPackage = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.InReplyTo:
                    header.InReplyTo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Join:
                    header.Join = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.MaxBreadth:
                    header.MaxBreadth = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.MaxForwards:
                    header.MaxForwards = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.MimeVersion:
                    header.MimeVersion = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.MinExpires:
                    header.MinExpires = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.MinSe:
                    header.MinSe = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Organization:
                    header.Organization = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PaccessNetworkInfo:
                    header.PaccessNetworkInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PanswerState:
                    header.PanswerState = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PassertedIdentity:
                    header.PassertedIdentity = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PassertedService:
                    header.PassertedService = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PassociatedUri:
                    header.PassociatedUri = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PcalledPartyId:
                    header.PcalledPartyId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PchargingFunctionAddresses:
                    header.PchargingFunctionAddresses = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PchargingVector:
                    header.PchargingVector = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PdcsTracePartyId:
                    header.PdcsTracePartyId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PdcsOsps:
                    header.PdcsOsps = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PdcsBillingInfo:
                    header.PdcsBillingInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PdcsLaes:
                    header.PdcsLaes = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PdcsRedirect:
                    header.PdcsRedirect = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PearlyMedia:
                    header.PearlyMedia = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PmediaAuthorization:
                    header.PmediaAuthorization = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PpreferredIdentity:
                    header.PpreferredIdentity = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PpreferredService:
                    header.PpreferredService = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PprivateNetworkIndication:
                    header.PprivateNetworkIndication = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PprofileKey:
                    header.PprofileKey = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PrefusedUriList:
                    header.PrefusedUriList = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PservedUser:
                    header.PservedUser = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PuserDatabase:
                    header.PuserDatabase = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PvisitedNetworkId:
                    header.PvisitedNetworkId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Path:
                    header.Path = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PermissionMissing:
                    header.PermissionMissing = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PolicyContact:
                    header.PolicyContact = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PolicyId:
                    header.PolicyId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Priority:
                    header.Priority = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.PrivAnswerMode:
                    header.PrivAnswerMode = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Privacy:
                    header.Privacy = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ProxyAuthenticate:
                    header.ProxyAuthenticate = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ProxyAuthorization:
                    header.ProxyAuthorization = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ProxyRequire:
                    header.ProxyRequire = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Rack:
                    header.Rack = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Reason:
                    header.Reason = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ReasonPhrase:
                    header.ReasonPhrase = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.RecordRoute:
                    header.RecordRoute = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.RecvInfo:
                    header.RecvInfo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ReferEventsAt:
                    header.ReferEventsAt = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ReferStub:
                    header.ReferStub = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ReferTo:
                    header.ReferTo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ReferredBy:
                    header.ReferredBy = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.RejectContact:
                    header.RejectContact = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Replaces:
                    header.Replaces = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ReplyTo:
                    header.ReplyTo = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.RequestDisposition:
                    header.RequestDisposition = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Require:
                    header.Require = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ResourcePriority:
                    header.ResourcePriority = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ResponseKey:
                    header.ResponseKey = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.RetryAfter:
                    header.RetryAfter = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Route:
                    header.Route = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Rseq:
                    header.Rseq = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.SecurityClient:
                    header.SecurityClient = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.SecurityServer:
                    header.SecurityServer = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.SecurityVerify:
                    header.SecurityVerify = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Server:
                    header.Server = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.ServiceRoute:
                    header.ServiceRoute = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.SessionExpires:
                    header.SessionExpires = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.SessionId:
                    header.SessionId = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.SipEtag:
                    header.SipEtag = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.SipIfMatch:
                    header.SipIfMatch = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Subject:
                    header.Subject = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.SubscriptionState:
                    header.SubscriptionState = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Supported:
                    header.Supported = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.SuppressIfMatch:
                    header.SuppressIfMatch = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.TargetDialog:
                    header.TargetDialog = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Timestamp:
                    header.Timestamp = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.To:
                    header.To = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.TriggerConstant:
                    header.TriggerConstant = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Unsupported:
                    header.Unsupported = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.UserAgent:
                    header.UserAgent = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.UserToUser:
                    header.UserToUser = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Via:
                    header.Via = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.Warning:
                    header.Warning = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                case FieldType.WwwAuthenticate:
                    header.WwwAuthenticate = field.Values.ToList();
                    return new ParseResult<HeaderDto>(header);
                default:
                    throw new NotImplementedException($"header field type {field.Name} is not implemented");
            }
        }

        private static ParseResult<T> ParseSingleValue<T>(IReadOnlyList<string> values, Func<string, ParseResult<T>> convert)
        {
            if (values.Count < 1)
                return new ParseResult<T>("the value for a header field is missing");

            if (values.Count > 1)
                return new ParseResult<T>("there is more than one value for a header field, which should have only one value");

            return convert(values.First());
        }

        private static ParseResult<int> ConvertStringToInt(string value)
        {
            int valueConverted;

            if (!int.TryParse(value, out valueConverted))
                return new ParseResult<int>($"could not convert the {value} into a string");

            return new ParseResult<int>(valueConverted);
        }

        #endregion

        #region private functions

        private ParseResult<List<HeaderField>> ParseHeaderFields(IReadOnlyList<string> lines, int headerEnd)
        {
            var fields = new List<HeaderField>();
            for (var i = 1; i <= headerEnd; ++i)
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

        #endregion
    }
}
