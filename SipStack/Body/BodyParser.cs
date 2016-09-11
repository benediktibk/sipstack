using System.Collections.Generic;

namespace SipStack.Body
{
    public interface IBodyParser
    {
        ParseResult<IBody> Parse(IList<string> lines, int startLine, int endLine);
    }
}
