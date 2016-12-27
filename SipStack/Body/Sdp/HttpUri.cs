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
                return new ParseResult<HttpUri>($"invalid uri: {data}");

            return new ParseResult<HttpUri>(new HttpUri(uri));
        }
    }
}
