using System.Linq;

namespace SipStack
{
    public class RequestLineParser
    {
        public ParseResult<RequestLine> Parse(string line)
        {
            var content = line.Split(' ');

            if (content.Count() != 3)
                return new ParseResult<RequestLine>(ParseError.InvalidRequestLine, "request line has invalid format");

            if (content[2] != "SIP/2.0")
                return new ParseResult<RequestLine>(ParseError.InvalidSipVersion, $"sip version {content[2]} is not supported");

            RequestMethod requestMethod;
            if (!RequestMethodUtils.TryParse(content[0], out requestMethod))
                return new ParseResult<RequestLine>(ParseError.InvalidRequestMethod, $"invalid request {content[0]}");

            return new ParseResult<RequestLine>(new RequestLine(requestMethod, content[1]));
        }
    }
}
