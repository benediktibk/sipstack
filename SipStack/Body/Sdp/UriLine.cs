using SipStack.Utils;
using System;

namespace SipStack.Body.Sdp
{
    public class UriLine : ILine
    {
        private readonly Uri _uri;

        public UriLine(Uri uri)
        {
            _uri = uri;
        }

        public Uri Uri => _uri;

        public static ParseResult<ILine> Parse(string data)
        {
            Uri uri;
            if (!Uri.TryCreate(data, UriKind.Absolute, out uri))
                return new ParseResult<ILine>($"invalid uri: {data}");

            return new ParseResult<ILine>(new UriLine(uri));
        }
    }
}
