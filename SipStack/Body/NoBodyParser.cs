﻿using SipStack.Utils;
using System.Collections.Generic;

namespace SipStack.Body
{
    public class NoBodyParser : IBodyParser
    {
        public ParseResult<IBody> Parse(IList<string> lines, int startLine, int endLine)
        {
            return ParseResult<IBody>.CreateSuccess(new NoBody());
        }
    }
}
