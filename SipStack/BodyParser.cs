using System.Collections.Generic;

namespace SipStack
{
    public class BodyParser
    {
        public ParseResult<IBody> Parse(IList<string> lines, int startLine, int endLine)
        {
            return new ParseResult<IBody>(new NoBody());
        }
    }
}
