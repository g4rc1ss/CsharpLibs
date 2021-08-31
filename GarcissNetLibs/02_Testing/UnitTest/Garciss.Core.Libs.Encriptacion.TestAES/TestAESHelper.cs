using System.IO;
using System.Security.Cryptography;
using System.Text;
using Garciss.Core.Libs.Encriptacion.AES;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Garciss.Core.Libs.Encriptacion.TestAES {
    [TestClass]
    public class TestAESHelper {
        private const string TEXTOPLANO = "Este texto es el que se va a encriptar :P /\\";
        private static readonly byte[] TEXTOCIFRADO = new byte[] { 0x9e, 0x21, 0x13, 0xe5, 0x41, 0x6e, 0xec, 0x8a, 0x70, 0xee, 0x00, 0x93,
                                                            0xca, 0x40, 0x18, 0x06, 0x19, 0xae, 0xee, 0xdb, 0xb5, 0x98, 0xf8, 0x7e,
                                                            0x2f, 0xd6, 0x0d, 0x21, 0x63, 0x79, 0xd6, 0xd2, 0xef, 0xfa, 0xc2, 0x0a,
                                                            0x45, 0x8c, 0x7d, 0x8a, 0x68, 0x70, 0x00, 0x45, 0x5a, 0xc5, 0x0f, 0xa3, };

        private static readonly byte[] IV = new byte[] { 0xed, 0xe0, 0xab, 0xe7, 0xa3, 0x5c, 0x9b, 0x6b, 0x14, 0x91,
                                                  0x94, 0x57, 0xee, 0x1e, 0xc4, 0xec, };

        private const string CIFRADO_TEXTO = "TextoCifradoClavePropia.aes";
        private const string IV_TEXTO = "IVpropia.aes";

        private const string CONTRASENIA = "contrasenia";

        private const string CIFRAR_ARCHIVO = "ArchivoToCrypt.txt";
        private const string DESCIFRAR_ARCHIVO = "ArchivoToDecrypt.aes";
        private const string IV_ARCHIVO = "IVarchivosPropia.aes";

        [ClassInitialize]
        public static void Create(TestContext testContext) {
            if (testContext is null) {
                throw new System.ArgumentNullException(nameof(testContext));
            }

            File.WriteAllBytes(CIFRADO_TEXTO, TEXTOCIFRADO);
            File.WriteAllBytes(IV_TEXTO, IV);
            File.WriteAllText(CIFRAR_ARCHIVO, TEXTOPLANO);
            File.WriteAllBytes(DESCIFRAR_ARCHIVO, TEXTOCIFRADO);
            File.WriteAllBytes(IV_ARCHIVO, IV);
        }


        [ClassCleanup]
        public static void Clean() {
            File.Delete(CIFRADO_TEXTO);
            File.Delete(IV_TEXTO);
            File.Delete(CIFRAR_ARCHIVO);
            File.Delete(DESCIFRAR_ARCHIVO);
            File.Delete(IV_ARCHIVO);
        }

        [TestMethod]
        public void CifradoTexto() {
            var cifrarTexto = new AESHelper();
            cifrarTexto.CreateKeyIV(CONTRASENIA);
            var textoCifrado = cifrarTexto.EncriptarTexto(TEXTOPLANO, cifrarTexto.Key, IV);

            File.WriteAllBytes(CIFRADO_TEXTO, textoCifrado);
            File.WriteAllBytes(IV_TEXTO, cifrarTexto.IV);

            for (var x = 0; x < File.ReadAllBytes(IV_TEXTO).Length && x < cifrarTexto.IV.Length; x++) {
                Assert.IsTrue(File.ReadAllBytes(IV_TEXTO)[x] == cifrarTexto.IV[x]);
            }

            for (int i = 0; i < textoCifrado.Length; i++) {
                Assert.IsTrue(textoCifrado[i] == TEXTOCIFRADO[i]);
            }
        }

        [TestMethod]
        public void DescifrarTexto() {
            var descifrarTexto = new AESHelper();

            var textoCifrado = File.ReadAllBytes(CIFRADO_TEXTO);
            var ivPropia = File.ReadAllBytes(IV_TEXTO);
            descifrarTexto.CreateKeyIV(CONTRASENIA);
            var textoDescifrado = descifrarTexto.DesencriptarTexto(textoCifrado, descifrarTexto.Key, ivPropia);

            Assert.IsTrue(textoDescifrado.Equals(TEXTOPLANO));
        }

        [TestMethod]
        public void CifradoArchivos() {
            var encriptarArchivo = new AESHelper();

            encriptarArchivo.CreateKeyIV(CONTRASENIA);
            encriptarArchivo.EncriptarFichero(CIFRAR_ARCHIVO, DESCIFRAR_ARCHIVO, encriptarArchivo.Key, IV);

            File.WriteAllBytes(IV_ARCHIVO, encriptarArchivo.IV);

            for (var x = 0; x < File.ReadAllBytes(IV_ARCHIVO).Length && x < encriptarArchivo.IV.Length; x++) {
                Assert.IsTrue(File.ReadAllBytes(IV_ARCHIVO)[x] == encriptarArchivo.IV[x]);
            }

            var textoCifrado = File.ReadAllBytes(DESCIFRAR_ARCHIVO);

            for (int i = 0; i < textoCifrado.Length; i++) {
                Assert.IsTrue(textoCifrado[i] == TEXTOCIFRADO[i]);
            }
        }

        [TestMethod]
        public void DecifradoArchivos() {
            var desencriptarArchivo = new AESHelper();
            desencriptarArchivo.CreateKeyIV(CONTRASENIA);
            var iv = File.ReadAllBytes(IV_ARCHIVO);
            desencriptarArchivo.DesencriptarFichero(DESCIFRAR_ARCHIVO, CIFRAR_ARCHIVO, desencriptarArchivo.Key, iv);

            Assert.IsTrue(File.ReadAllText(CIFRAR_ARCHIVO) == TEXTOPLANO);
        }
    }
}
