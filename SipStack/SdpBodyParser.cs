using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack
{
    public class SdpBodyParser : IBodyParser
    {
        public ParseResult<IBody> Parse(IList<string> lines, int startLine, int endLine)
        {
            throw new NotImplementedException();
        }
    }
}
