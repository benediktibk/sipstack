using SipStack.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static ParseResult<EncryptionKey> Parse(string data)
        {
            if (data == "prompt")
                return ParseResult<EncryptionKey>.CreateSuccess(new EncryptionKey(EncryptionKeyType.Prompt, ""));

            var pattern = "^([^:]*):(.*)$";
            var match = Regex.Matches(data, pattern);

            if (match.Count != 1)
                return ParseResult<EncryptionKey>.CreateError($"malformed encryption key line: {data}");

            var keyTypeString = match[0].Groups[1].Value;
            var key = match[0].Groups[2].Value;
            EncryptionKeyType keyType;

            if (!EncryptionKeyTypeUtils.TryParse(keyTypeString, out keyType))
                return ParseResult<EncryptionKey>.CreateError($"invalid key type: {keyTypeString}");

            return ParseResult<EncryptionKey>.CreateSuccess(new EncryptionKey(keyType, key));
        }
    }
}
