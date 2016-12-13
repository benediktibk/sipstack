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
                    break;
                case FieldType.AcceptContact:
                    header.AcceptContact = field.Values.ToList();
                    break;
                case FieldType.AcceptEncoding:
                    header.AcceptEncoding = field.Values.ToList();
                    break;
                case FieldType.AcceptLanguage:
                    header.AcceptLanguage = field.Values.ToList();
                    break;
                case FieldType.AcceptResourcePriority:
                    header.AcceptResourcePriority = field.Values.ToList();
                    break;
                case FieldType.AlertInfo:
                    header.AlertInfo = field.Values.ToList();
                    break;
                case FieldType.Allow:
                    var allow = ParseMultipleValues(field.Values, RequestMethodUtils.Parse);

                    if (allow.IsError)
                        return allow.ToParseResult<HeaderDto>();

                    header.Allow = allow.Result;
                    break;
                case FieldType.AllowEvents:
                    header.AllowEvents = field.Values.ToList();
                    break;
                case FieldType.AnswerMode:
                    header.AnswerMode = field.Values.ToList();
                    break;
                case FieldType.AuthenticationInfo:
                    header.AuthenticationInfo = field.Values.ToList();
                    break;
                case FieldType.Authorization:
                    header.Authorization = field.Values.ToList();
                    break;
                case FieldType.CallId:
                    header.CallId = field.Values.ToList();
                    break;
                case FieldType.CallInfo:
                    header.CallInfo = field.Values.ToList();
                    break;
                case FieldType.Contact:
                    header.Contact = field.Values.ToList();
                    break;
                case FieldType.ContentDisposition:
                    header.ContentDisposition = field.Values.ToList();
                    break;
                case FieldType.ContentEncoding:
                    header.ContentEncoding = field.Values.ToList();
                    break;
                case FieldType.ContentLanguage:
                    header.ContentLanguage = field.Values.ToList();
                    break;
                case FieldType.ContentLength:
                    var contentLength = ParseMandatorySingleValue(field.Values, ConvertStringToInt);

                    if (contentLength.IsError)
                        return contentLength.ToParseResult<HeaderDto>();

                    header.ContentLength = contentLength.Result;
                    break;
                case FieldType.ContentType:
                    var contentType = ParseMandatorySingleValue(field.Values, (x) => { return new ParseResult<string>(x, true); });

                    if (contentType.IsError)
                        return contentType.ToParseResult<HeaderDto>();

                    header.ContentType = contentType.Result;
                    break;
                case FieldType.Cseq:
                    header.Cseq = field.Values.ToList();
                    break;
                case FieldType.Date:
                    header.Date = field.Values.ToList();
                    break;
                case FieldType.Encryption:
                    header.Encryption = field.Values.ToList();
                    break;
                case FieldType.ErrorInfo:
                    header.ErrorInfo = field.Values.ToList();
                    break;
                case FieldType.Event:
                    header.Event = field.Values.ToList();
                    break;
                case FieldType.Expires:
                    header.Expires = field.Values.ToList();
                    break;
                case FieldType.FeatureCaps:
                    header.FeatureCaps = field.Values.ToList();
                    break;
                case FieldType.FlowTimer:
                    header.FlowTimer = field.Values.ToList();
                    break;
                case FieldType.From:
                    header.From = field.Values.ToList();
                    break;
                case FieldType.Geolocation:
                    header.Geolocation = field.Values.ToList();
                    break;
                case FieldType.GeolocationError:
                    header.GeolocationError = field.Values.ToList();
                    break;
                case FieldType.GeolocationRouting:
                    header.GeolocationRouting = field.Values.ToList();
                    break;
                case FieldType.Hide:
                    header.Hide = field.Values.ToList();
                    break;
                case FieldType.HistoryInfo:
                    header.HistoryInfo = field.Values.ToList();
                    break;
                case FieldType.Identity:
                    header.Identity = field.Values.ToList();
                    break;
                case FieldType.IdentityInfo:
                    header.IdentityInfo = field.Values.ToList();
                    break;
                case FieldType.InfoPackage:
                    header.InfoPackage = field.Values.ToList();
                    break;
                case FieldType.InReplyTo:
                    header.InReplyTo = field.Values.ToList();
                    break;
                case FieldType.Join:
                    header.Join = field.Values.ToList();
                    break;
                case FieldType.MaxBreadth:
                    header.MaxBreadth = field.Values.ToList();
                    break;
                case FieldType.MaxForwards:
                    header.MaxForwards = field.Values.ToList();
                    break;
                case FieldType.MimeVersion:
                    header.MimeVersion = field.Values.ToList();
                    break;
                case FieldType.MinExpires:
                    header.MinExpires = field.Values.ToList();
                    break;
                case FieldType.MinSe:
                    header.MinSe = field.Values.ToList();
                    break;
                case FieldType.Organization:
                    header.Organization = field.Values.ToList();
                    break;
                case FieldType.PaccessNetworkInfo:
                    header.PaccessNetworkInfo = field.Values.ToList();
                    break;
                case FieldType.PanswerState:
                    header.PanswerState = field.Values.ToList();
                    break;
                case FieldType.PassertedIdentity:
                    header.PassertedIdentity = field.Values.ToList();
                    break;
                case FieldType.PassertedService:
                    header.PassertedService = field.Values.ToList();
                    break;
                case FieldType.PassociatedUri:
                    header.PassociatedUri = field.Values.ToList();
                    break;
                case FieldType.PcalledPartyId:
                    header.PcalledPartyId = field.Values.ToList();
                    break;
                case FieldType.PchargingFunctionAddresses:
                    header.PchargingFunctionAddresses = field.Values.ToList();
                    break;
                case FieldType.PchargingVector:
                    header.PchargingVector = field.Values.ToList();
                    break;
                case FieldType.PdcsTracePartyId:
                    header.PdcsTracePartyId = field.Values.ToList();
                    break;
                case FieldType.PdcsOsps:
                    header.PdcsOsps = field.Values.ToList();
                    break;
                case FieldType.PdcsBillingInfo:
                    header.PdcsBillingInfo = field.Values.ToList();
                    break;
                case FieldType.PdcsLaes:
                    header.PdcsLaes = field.Values.ToList();
                    break;
                case FieldType.PdcsRedirect:
                    header.PdcsRedirect = field.Values.ToList();
                    break;
                case FieldType.PearlyMedia:
                    header.PearlyMedia = field.Values.ToList();
                    break;
                case FieldType.PmediaAuthorization:
                    header.PmediaAuthorization = field.Values.ToList();
                    break;
                case FieldType.PpreferredIdentity:
                    header.PpreferredIdentity = field.Values.ToList();
                    break;
                case FieldType.PpreferredService:
                    header.PpreferredService = field.Values.ToList();
                    break;
                case FieldType.PprivateNetworkIndication:
                    header.PprivateNetworkIndication = field.Values.ToList();
                    break;
                case FieldType.PprofileKey:
                    header.PprofileKey = field.Values.ToList();
                    break;
                case FieldType.PrefusedUriList:
                    header.PrefusedUriList = field.Values.ToList();
                    break;
                case FieldType.PservedUser:
                    header.PservedUser = field.Values.ToList();
                    break;
                case FieldType.PuserDatabase:
                    header.PuserDatabase = field.Values.ToList();
                    break;
                case FieldType.PvisitedNetworkId:
                    header.PvisitedNetworkId = field.Values.ToList();
                    break;
                case FieldType.Path:
                    header.Path = field.Values.ToList();
                    break;
                case FieldType.PermissionMissing:
                    header.PermissionMissing = field.Values.ToList();
                    break;
                case FieldType.PolicyContact:
                    header.PolicyContact = field.Values.ToList();
                    break;
                case FieldType.PolicyId:
                    header.PolicyId = field.Values.ToList();
                    break;
                case FieldType.Priority:
                    header.Priority = field.Values.ToList();
                    break;
                case FieldType.PrivAnswerMode:
                    header.PrivAnswerMode = field.Values.ToList();
                    break;
                case FieldType.Privacy:
                    header.Privacy = field.Values.ToList();
                    break;
                case FieldType.ProxyAuthenticate:
                    header.ProxyAuthenticate = field.Values.ToList();
                    break;
                case FieldType.ProxyAuthorization:
                    header.ProxyAuthorization = field.Values.ToList();
                    break;
                case FieldType.ProxyRequire:
                    header.ProxyRequire = field.Values.ToList();
                    break;
                case FieldType.Rack:
                    header.Rack = field.Values.ToList();
                    break;
                case FieldType.Reason:
                    header.Reason = field.Values.ToList();
                    break;
                case FieldType.ReasonPhrase:
                    header.ReasonPhrase = field.Values.ToList();
                    break;
                case FieldType.RecordRoute:
                    header.RecordRoute = field.Values.ToList();
                    break;
                case FieldType.RecvInfo:
                    header.RecvInfo = field.Values.ToList();
                    break;
                case FieldType.ReferEventsAt:
                    header.ReferEventsAt = field.Values.ToList();
                    break;
                case FieldType.ReferStub:
                    header.ReferStub = field.Values.ToList();
                    break;
                case FieldType.ReferTo:
                    header.ReferTo = field.Values.ToList();
                    break;
                case FieldType.ReferredBy:
                    header.ReferredBy = field.Values.ToList();
                    break;
                case FieldType.RejectContact:
                    header.RejectContact = field.Values.ToList();
                    break;
                case FieldType.Replaces:
                    header.Replaces = field.Values.ToList();
                    break;
                case FieldType.ReplyTo:
                    header.ReplyTo = field.Values.ToList();
                    break;
                case FieldType.RequestDisposition:
                    header.RequestDisposition = field.Values.ToList();
                    break;
                case FieldType.Require:
                    header.Require = field.Values.ToList();
                    break;
                case FieldType.ResourcePriority:
                    header.ResourcePriority = field.Values.ToList();
                    break;
                case FieldType.ResponseKey:
                    header.ResponseKey = field.Values.ToList();
                    break;
                case FieldType.RetryAfter:
                    header.RetryAfter = field.Values.ToList();
                    break;
                case FieldType.Route:
                    header.Route = field.Values.ToList();
                    break;
                case FieldType.Rseq:
                    header.Rseq = field.Values.ToList();
                    break;
                case FieldType.SecurityClient:
                    header.SecurityClient = field.Values.ToList();
                    break;
                case FieldType.SecurityServer:
                    header.SecurityServer = field.Values.ToList();
                    break;
                case FieldType.SecurityVerify:
                    header.SecurityVerify = field.Values.ToList();
                    break;
                case FieldType.Server:
                    header.Server = field.Values.ToList();
                    break;
                case FieldType.ServiceRoute:
                    header.ServiceRoute = field.Values.ToList();
                    break;
                case FieldType.SessionExpires:
                    header.SessionExpires = field.Values.ToList();
                    break;
                case FieldType.SessionId:
                    header.SessionId = field.Values.ToList();
                    break;
                case FieldType.SipEtag:
                    header.SipEtag = field.Values.ToList();
                    break;
                case FieldType.SipIfMatch:
                    header.SipIfMatch = field.Values.ToList();
                    break;
                case FieldType.Subject:
                    header.Subject = field.Values.ToList();
                    break;
                case FieldType.SubscriptionState:
                    header.SubscriptionState = field.Values.ToList();
                    break;
                case FieldType.Supported:
                    header.Supported = field.Values.ToList();
                    break;
                case FieldType.SuppressIfMatch:
                    header.SuppressIfMatch = field.Values.ToList();
                    break;
                case FieldType.TargetDialog:
                    header.TargetDialog = field.Values.ToList();
                    break;
                case FieldType.Timestamp:
                    header.Timestamp = field.Values.ToList();
                    break;
                case FieldType.To:
                    header.To = field.Values.ToList();
                    break;
                case FieldType.TriggerConstant:
                    header.TriggerConstant = field.Values.ToList();
                    break;
                case FieldType.Unsupported:
                    header.Unsupported = field.Values.ToList();
                    break;
                case FieldType.UserAgent:
                    header.UserAgent = field.Values.ToList();
                    break;
                case FieldType.UserToUser:
                    header.UserToUser = field.Values.ToList();
                    break;
                case FieldType.Via:
                    header.Via = field.Values.ToList();
                    break;
                case FieldType.Warning:
                    header.Warning = field.Values.ToList();
                    break;
                case FieldType.WwwAuthenticate:
                    header.WwwAuthenticate = field.Values.ToList();
                    break;
                default:
                    throw new NotImplementedException($"header field type {field.Name} is not implemented");
            }

            return new ParseResult<HeaderDto>(header);
        }

        private static ParseResult<T> ParseMandatorySingleValue<T>(IReadOnlyList<string> values, Func<string, ParseResult<T>> convert)
        {
            if (values.Count < 1)
                return new ParseResult<T>("the value for a header field is missing");

            if (values.Count > 1)
                return new ParseResult<T>("there is more than one value for a header field, which should have only one value");

            return convert(values.First());
        }

        private static ParseResult<List<T>> ParseMultipleValues<T>(IReadOnlyList<string> values, Func<string, ParseResult<T>> convert)
        {
            var result = new List<T>(values.Count);

            foreach (var value in values)
            {
                var parseResult = convert(value);

                if (parseResult.IsError)
                    return parseResult.ToParseResult<List<T>>();

                result.Add(parseResult.Result);
            }

            return new ParseResult<List<T>>(result);
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
