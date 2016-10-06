using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class EncryptionKeyLineTest
    {
        [TestMethod]
        public void Parse_Prompt_TypePromptAndEmptyKey()
        {
            var line = EncryptionKeyLine.Parse("prompt");

            var encryptionKeyLine = line.Result as EncryptionKeyLine;
            encryptionKeyLine.KeyType.Should().Be(EncryptionKeyType.Prompt);
            encryptionKeyLine.Key.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void Parse_Clear_TypeClearAndCorrectKey()
        {
            var line = EncryptionKeyLine.Parse(@"clear:Adfas23947:adfo/)/=");

            var encryptionKeyLine = line.Result as EncryptionKeyLine;
            encryptionKeyLine.KeyType.Should().Be(EncryptionKeyType.Clear);
            encryptionKeyLine.Key.Should().Be(@"Adfas23947:adfo/)/=");
        }

        [TestMethod]
        public void Parse_Base64_TypeBase64AndCorrectKey()
        {
            var line = EncryptionKeyLine.Parse(@"base64:Y2xlYXI6QWRmYXMyMzk0NzphZGZvLykvPQ==");

            var encryptionKeyLine = line.Result as EncryptionKeyLine;
            encryptionKeyLine.KeyType.Should().Be(EncryptionKeyType.Base64);
            encryptionKeyLine.Key.Should().Be(@"Y2xlYXI6QWRmYXMyMzk0NzphZGZvLykvPQ==");
        }

        [TestMethod]
        public void Parse_Uri_TypeUriAndCorrectKey()
        {
            var line = EncryptionKeyLine.Parse(@"uri:http://asdf.asdf.asdf/asdf?asdf=dfad");

            var encryptionKeyLine = line.Result as EncryptionKeyLine;
            encryptionKeyLine.KeyType.Should().Be(EncryptionKeyType.Uri);
            encryptionKeyLine.Key.Should().Be(@"http://asdf.asdf.asdf/asdf?asdf=dfad");
        }

        [TestMethod]
        public void Parse_PromptWithData_TypePromptAndCorrectKey()
        {
            var line = EncryptionKeyLine.Parse(@"prompt:http://asdf.asdf.asdf/asdf?asdf=dfad");

            var encryptionKeyLine = line.Result as EncryptionKeyLine;
            encryptionKeyLine.KeyType.Should().Be(EncryptionKeyType.Prompt);
            encryptionKeyLine.Key.Should().Be(@"http://asdf.asdf.asdf/asdf?asdf=dfad");
        }

        [TestMethod]
        public void Parse_InvalidType_Error()
        {
            var line = EncryptionKeyLine.Parse(@"INVALIDSHIT:http://asdf.asdf.asdf/asdf?asdf=dfad");

            line.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_MissingColon_Error()
        {
            var line = EncryptionKeyLine.Parse(@"clearaad");

            line.IsError.Should().BeTrue();
        }
    }
}
