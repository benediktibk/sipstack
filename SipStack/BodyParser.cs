using System.Collections.Generic;

namespace SipStack
{
    public interface IBodyParser
    {
        ParseResult<IBody> Parse(IList<string> lines, int startLine, int endLine);
    }
}
