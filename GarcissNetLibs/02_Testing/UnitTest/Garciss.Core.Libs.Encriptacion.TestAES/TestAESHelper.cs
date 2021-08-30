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
        private const string IV_PROPIA_TEXTO = "IVpropia.aes";

        private const string CONTRASENIA = "contrasenia";

        private const string CIFRAR_ARCHIVO = "ArchivoToCrypt.txt";
        private const string DESCIFRAR_ARCHIVO = "ArchivoToDecrypt.aes";
        private const string IV_PROPIA_ARCHIVO = "IVarchivosPropia.aes";

        [ClassInitialize]
        public static void Create(TestContext testContext) {
            if (testContext is null) {
                throw new System.ArgumentNullException(nameof(testContext));
            }

            File.WriteAllBytes(CIFRADO_TEXTO, TEXTOCIFRADO);
            File.WriteAllBytes(IV_PROPIA_TEXTO, IV);
            File.WriteAllText(CIFRAR_ARCHIVO, TEXTOPLANO);
            File.WriteAllBytes(DESCIFRAR_ARCHIVO, TEXTOCIFRADO);
            File.WriteAllBytes(IV_PROPIA_ARCHIVO, IV);
        }


        [ClassCleanup]
        public static void Clean() {
            File.Delete(CIFRADO_TEXTO);
            File.Delete(IV_PROPIA_TEXTO);
            File.Delete(CIFRAR_ARCHIVO);
            File.Delete(DESCIFRAR_ARCHIVO);
            File.Delete(IV_PROPIA_ARCHIVO);
        }

        [TestMethod]
        public void CifradoTexto() {
            var cifrarTextoClavePropia = new AESHelper();
            cifrarTextoClavePropia.CreateKeyIV(CONTRASENIA);
            var textoCifrado = cifrarTextoClavePropia.EncriptarTexto(TEXTOPLANO, cifrarTextoClavePropia.Key, IV);

            File.WriteAllBytes(CIFRADO_TEXTO, textoCifrado);
            File.WriteAllBytes(IV_PROPIA_TEXTO, cifrarTextoClavePropia.IV);

            for (var x = 0; x < File.ReadAllBytes(IV_PROPIA_TEXTO).Length && x < cifrarTextoClavePropia.IV.Length; x++) {
                Assert.IsTrue(File.ReadAllBytes(IV_PROPIA_TEXTO)[x] == cifrarTextoClavePropia.IV[x]);
            }

            for (int i = 0; i < textoCifrado.Length; i++) {
                Assert.IsTrue(textoCifrado[i] == TEXTOCIFRADO[i]);
            }

            Assert.IsTrue(
                cifrarTextoClavePropia.Key == File.ReadAllBytes(CIFRADO_TEXTO) &&
                cifrarTextoClavePropia.IV.Length == 16
            );
        }

        [TestMethod]
        public void DescifrarTexto() {
            var descifrarTextoClavePropia = new AESHelper();

            var textoCifradoClavePropia = File.ReadAllBytes(CIFRADO_TEXTO);
            var ivPropia = File.ReadAllBytes(IV_PROPIA_TEXTO);
            descifrarTextoClavePropia.CreateKeyIV(CONTRASENIA);
            var textoDescifradoPropio = descifrarTextoClavePropia.DesencriptarTexto(textoCifradoClavePropia, descifrarTextoClavePropia.Key, ivPropia);

            Assert.IsTrue(
                textoDescifradoPropio == TEXTOPLANO &&
                descifrarTextoClavePropia.IV.Length == 16
            );

        }

        [TestMethod]
        public void CifradoArchivos() {
            var encriptarArchivoClavePropia = new AESHelper();

            encriptarArchivoClavePropia.CreateKeyIV(CONTRASENIA);
            encriptarArchivoClavePropia.EncriptarFichero(CIFRAR_ARCHIVO, DESCIFRAR_ARCHIVO, encriptarArchivoClavePropia.Key, IV);

            File.WriteAllBytes(IV_PROPIA_ARCHIVO, encriptarArchivoClavePropia.IV);

            for (var x = 0; x < File.ReadAllBytes(IV_PROPIA_ARCHIVO).Length && x < encriptarArchivoClavePropia.IV.Length; x++) {
                Assert.IsTrue(File.ReadAllBytes(IV_PROPIA_ARCHIVO)[x] == encriptarArchivoClavePropia.IV[x]);
            }

            var textoCifrado = File.ReadAllBytes(DESCIFRAR_ARCHIVO);

            for (int i = 0; i < textoCifrado.Length; i++) {
                Assert.IsTrue(textoCifrado[i] == TEXTOCIFRADO[i]);
            }
        }

        [TestMethod]
        public void DecifradoArchivos() {
            var desencriptarArchivoClavePropia = new AESHelper();
            desencriptarArchivoClavePropia.CreateKeyIV(CONTRASENIA);
            var ivPropio = File.ReadAllBytes(IV_PROPIA_ARCHIVO);
            desencriptarArchivoClavePropia.DesencriptarFichero(DESCIFRAR_ARCHIVO, CIFRAR_ARCHIVO, desencriptarArchivoClavePropia.Key, ivPropio);

            Assert.IsTrue(File.ReadAllText(CIFRAR_ARCHIVO) == TEXTOPLANO);
        }
    }
}
