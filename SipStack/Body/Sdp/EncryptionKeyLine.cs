using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack.Body.Sdp
{
    public class EncryptionKeyLine : ILine
    {
        public static ParseResult<ILine> CreateFrom(string data)
        {
            throw new NotImplementedException();
        }
    }
}
