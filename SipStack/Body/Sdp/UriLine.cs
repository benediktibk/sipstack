using SipStack.Utils;
using System;

namespace SipStack.Body.Sdp
{
    public class UriLine : ILine
    {
        public UriLine(Uri uri)
        {
            Uri = uri;
        }

        public Uri Uri { get; }

        public static ParseResult<ILine> Parse(string data)
        {
            Uri uri;
            if (!Uri.TryCreate(data, UriKind.Absolute, out uri))
                return new ParseResult<ILine>($"invalid uri: {data}");

            return new ParseResult<ILine>(new UriLine(uri));
        }
    }
}
