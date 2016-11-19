using SipStack.Utils;
using System.Text.RegularExpressions;

namespace SipStack.Body.Sdp
{
    public class EncryptionKeyLine : ILine
    {
        public EncryptionKeyLine(EncryptionKeyType keyType, string key)
        {
            KeyType = keyType;
            Key = key;
        }

        public EncryptionKeyType KeyType { get; }
        public string Key { get; }

        public static ParseResult<ILine> Parse(string data)
        {
            if (data == "prompt")
                return new ParseResult<ILine>(new EncryptionKeyLine(EncryptionKeyType.Prompt, ""));

            var pattern = "^([^:]*):(.*)$";
            var match = Regex.Matches(data, pattern);

            if (match.Count != 1)
                return new ParseResult<ILine>($"malformed encryption key line: {data}");

            var keyTypeString = match[0].Groups[1].Value;
            var key = match[0].Groups[2].Value;
            EncryptionKeyType keyType;

            if (!EncryptionKeyTypeUtils.TryParse(keyTypeString, out keyType))
                return new ParseResult<ILine>($"invalid key type: {keyTypeString}");

            return new ParseResult<ILine>(new EncryptionKeyLine(keyType, key));
        }
    }
}
