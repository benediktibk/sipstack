using SipStack.Utils;
using System;

namespace SipStack.Body.Sdp
{
    public class HttpUri
    {
        public HttpUri(Uri uri)
        {
            Uri = uri;
        }

        public Uri Uri { get; }

        public static ParseResult<HttpUri> Parse(string data)
        {
            Uri uri;
            if (!Uri.TryCreate(data, UriKind.Absolute, out uri))
                return ParseResult<HttpUri>.CreateError($"invalid uri: {data}");

            return ParseResult<HttpUri>.CreateSuccess(new HttpUri(uri));
        }
    }
}
