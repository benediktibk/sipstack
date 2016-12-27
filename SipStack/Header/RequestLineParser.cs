using SipStack.Utils;
using System.Linq;
using System.Text.RegularExpressions;

namespace SipStack.Header
{
    public class RequestLineParser
    {
        public ParseResult<RequestLine> Parse(string line)
        {
            var pattern = @"^(.*) (.*) (.*)$";
            var matches = Regex.Matches(line, pattern);

            if (matches.Count != 1)
                return ParseResult<RequestLine>.CreateError($"request line '{line}' has an invalid format");

            var requestMethod = matches[0].Groups[1].Value;
            var requestUri = matches[0].Groups[2].Value;
            var sipVersion = matches[0].Groups[3].Value;

            if (sipVersion != "SIP/2.0")
                return ParseResult<RequestLine>.CreateError($"sip version {sipVersion} is not supported");

            RequestMethod requestMethodParsed;
            if (!RequestMethodUtils.TryParse(requestMethod, out requestMethodParsed))
                return ParseResult<RequestLine>.CreateError($"invalid request {requestMethod}");

            return ParseResult<RequestLine>.CreateSuccess(new RequestLine(requestMethodParsed, requestUri));
        }
    }
}
