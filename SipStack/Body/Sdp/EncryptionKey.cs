using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack.Body.Sdp
{
    public class EncryptionKey
    {
        public EncryptionKey(EncryptionKeyType keyType, string key)
        {
            KeyType = keyType;
            Key = key;
        }

        public EncryptionKeyType KeyType { get; }
        public string Key { get; }
    }
}
