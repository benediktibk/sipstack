using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SipStack.Body.Sdp;

namespace SipStackTest.Body.Sdp
{
    [TestClass]
    public class EncryptionKeyTest
    {
        [TestMethod]
        public void Parse_Prompt_TypePromptAndEmptyKey()
        {
            var encryptionKey = EncryptionKey.Parse("prompt");
            
            encryptionKey.Result.KeyType.Should().Be(EncryptionKeyType.Prompt);
            encryptionKey.Result.Key.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void Parse_Clear_TypeClearAndCorrectKey()
        {
            var encryptionKey = EncryptionKey.Parse(@"clear:Adfas23947:adfo/)/=");
            
            encryptionKey.Result.KeyType.Should().Be(EncryptionKeyType.Clear);
            encryptionKey.Result.Key.Should().Be(@"Adfas23947:adfo/)/=");
        }

        [TestMethod]
        public void Parse_Base64_TypeBase64AndCorrectKey()
        {
            var encryptionKey = EncryptionKey.Parse(@"base64:Y2xlYXI6QWRmYXMyMzk0NzphZGZvLykvPQ==");
            
            encryptionKey.Result.KeyType.Should().Be(EncryptionKeyType.Base64);
            encryptionKey.Result.Key.Should().Be(@"Y2xlYXI6QWRmYXMyMzk0NzphZGZvLykvPQ==");
        }

        [TestMethod]
        public void Parse_Uri_TypeUriAndCorrectKey()
        {
            var encryptionKey = EncryptionKey.Parse(@"uri:http://asdf.asdf.asdf/asdf?asdf=dfad");
            
            encryptionKey.Result.KeyType.Should().Be(EncryptionKeyType.Uri);
            encryptionKey.Result.Key.Should().Be(@"http://asdf.asdf.asdf/asdf?asdf=dfad");
        }

        [TestMethod]
        public void Parse_PromptWithData_TypePromptAndCorrectKey()
        {
            var encryptionKey = EncryptionKey.Parse(@"prompt:http://asdf.asdf.asdf/asdf?asdf=dfad");
            
            encryptionKey.Result.KeyType.Should().Be(EncryptionKeyType.Prompt);
            encryptionKey.Result.Key.Should().Be(@"http://asdf.asdf.asdf/asdf?asdf=dfad");
        }

        [TestMethod]
        public void Parse_InvalidType_Error()
        {
            var encryptionKey = EncryptionKey.Parse(@"INVALIDSHIT:http://asdf.asdf.asdf/asdf?asdf=dfad");

            encryptionKey.IsError.Should().BeTrue();
        }

        [TestMethod]
        public void Parse_MissingColon_Error()
        {
            var encryptionKey = EncryptionKey.Parse(@"clearaad");

            encryptionKey.IsError.Should().BeTrue();
        }
    }
}
