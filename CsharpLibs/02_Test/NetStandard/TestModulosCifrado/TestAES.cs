using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModulosCifrado;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TestModulosCifrado {
    [TestClass]
    public class TestAES {
        private const string _TEXTOPLANO = "Este texto es el que se va a encriptar :P /\\";
        [TestMethod]
        public void CifradoTexto() {
            //----------------------CON CLAVE ALEATORIA------------------\\
            var cifrarTextoClaveRandom = new CifradoAES();
            byte[] textoCifrado = cifrarTextoClaveRandom.EncriptarTexto(_TEXTOPLANO);

            File.WriteAllBytes("Key.aes", cifrarTextoClaveRandom.Key);
            File.WriteAllBytes("IV.aes", cifrarTextoClaveRandom.IV);
            File.WriteAllBytes("TextoCifradoClaveRandom.aes", textoCifrado);


            for (int x = 0; x < File.ReadAllBytes("Key.aes").Length && x < cifrarTextoClaveRandom.Key.Length; x++)
                Assert.IsTrue(File.ReadAllBytes("Key.aes")[x] == cifrarTextoClaveRandom.Key[x]);

            for (int x = 0; x < File.ReadAllBytes("IV.aes").Length && x < cifrarTextoClaveRandom.IV.Length; x++)
                Assert.IsTrue(File.ReadAllBytes("IV.aes")[x] == cifrarTextoClaveRandom.IV[x]);

            Assert.IsTrue(Encoding.UTF8.GetString(textoCifrado) != _TEXTOPLANO && File.Exists("TextoCifradoClaveRandom.aes"));

            //----------------------CON CLAVE PROPIA---------------------\\
            var cifrarTextoClavePropia = new CifradoAES();
            using (HashAlgorithm hash = SHA256.Create()) {
                byte[] keyHashByte = hash.ComputeHash(Encoding.Unicode.GetBytes("contrasenia"));

                textoCifrado = cifrarTextoClavePropia.EncriptarTexto(

                    text: _TEXTOPLANO,
                    keyParameter: keyHashByte,
                    iVparameter: cifrarTextoClavePropia.IV
                );
                File.WriteAllBytes("TextoCifradoClavePropia.aes", textoCifrado);
                File.WriteAllBytes("IVpropia.aes", cifrarTextoClavePropia.IV);

                for (int x = 0; x < File.ReadAllBytes("IVpropia.aes").Length && x < cifrarTextoClavePropia.IV.Length; x++)
                    Assert.IsTrue(File.ReadAllBytes("IVpropia.aes")[x] == cifrarTextoClavePropia.IV[x]);

                Assert.IsTrue(
                    cifrarTextoClavePropia.Key == keyHashByte &&
                    Encoding.UTF8.GetString(textoCifrado) != _TEXTOPLANO &&
                    cifrarTextoClavePropia.IV.Length == 16 &&
                    File.Exists("TextoCifradoClavePropia.aes")
                );
            }
        }

        [TestMethod]
        public void DescifrarTexto() {
            CifradoTexto();
            //----------------------CON CLAVE ALEATORIA------------------\\
            var descifrarTextoClaveRandom = new CifradoAES();
            string textoDescifrado = descifrarTextoClaveRandom.DesencriptarTexto(
                cipherText: File.ReadAllBytes("TextoCifradoClaveRandom.aes"),
                keyParameter: File.ReadAllBytes("Key.aes"),
                iVparameter: File.ReadAllBytes("IV.aes")
            );
            File.Delete("Key.aes"); File.Delete("IV.aes");
            File.Delete("TextoCifradoClaveRandom.aes");

            Assert.IsTrue(
                textoDescifrado == _TEXTOPLANO &&
                !File.Exists("Key.aes") &&
                !File.Exists("IV.aes") &&
                !File.Exists("TextoCifradoClaveRandom.aes")
            );

            //----------------------CON CLAVE PROPIA---------------------\\
            var descifrarTextoClavePropia = new CifradoAES();
            using (HashAlgorithm hash = SHA256.Create()) {
                byte[] keyHashByte = hash.ComputeHash(Encoding.Unicode.GetBytes("contrasenia"));

                string textoDescifradoPropio = descifrarTextoClavePropia.DesencriptarTexto(
                    cipherText: File.ReadAllBytes("TextoCifradoClavePropia.aes"),
                    keyParameter: keyHashByte,
                    iVparameter: File.ReadAllBytes("IVpropia.aes")
                );
                File.Delete("TextoCifradoClavePropia.aes"); File.Delete("IVpropia.aes");

                Assert.IsTrue(
                    descifrarTextoClavePropia.Key == keyHashByte &&
                    textoDescifradoPropio == _TEXTOPLANO &&
                    descifrarTextoClavePropia.IV.Length == 16 &&
                    !File.Exists("TextoCifradoClavePropia.aes") &&
                    !File.Exists("IVpropia.aes")
                );
            }
        }

        [TestMethod]
        public void CifradoArchivos() {
            //----------------------CON CLAVE ALEATORIA------------------\\
            string archivoAES_TXT = "archivo.txt"; string archivoAES_TXT_Propia = "archivoPropia.txt";
            string archivoKeyRandom = "KeyArchivos.aes"; string archivoIVRandom = "IVarchivos.aes";
            string archivoIVPropia = "IVarchivosPropia.aes";
            try {
                File.WriteAllText(archivoAES_TXT, _TEXTOPLANO);
                File.WriteAllText(archivoAES_TXT_Propia, _TEXTOPLANO);

                var encriptarFicheroClaveRandom = new CifradoAES();
                encriptarFicheroClaveRandom.CriptografiaFicheros(path: archivoAES_TXT, modo: CifrarDescifrar.cifrar);
                File.WriteAllBytes(archivoKeyRandom, encriptarFicheroClaveRandom.Key);
                File.WriteAllBytes(archivoIVRandom, encriptarFicheroClaveRandom.IV);
                //-------------------------------------------------------

                for (int x = 0; x < File.ReadAllBytes(archivoKeyRandom).Length && x < encriptarFicheroClaveRandom.Key.Length; x++)
                    Assert.IsTrue(encriptarFicheroClaveRandom.Key[x] == File.ReadAllBytes(archivoKeyRandom)[x]);

                for (int x = 0; x < File.ReadAllBytes(archivoIVRandom).Length && x < encriptarFicheroClaveRandom.IV.Length; x++)
                    Assert.IsTrue(File.ReadAllBytes(archivoIVRandom)[x] == encriptarFicheroClaveRandom.IV[x]);

                Assert.IsTrue(
                    File.Exists($"{archivoAES_TXT}.crypt") &&
                    Encoding.Unicode.GetString(File.ReadAllBytes($"{archivoAES_TXT}.crypt")) != _TEXTOPLANO
                );
                //----------------------CON CLAVE PROPIA---------------------\\
                var encriptarArchivoClavePropia = new CifradoAES();
                using (HashAlgorithm hash = SHA256.Create()) {
                    byte[] keyHashByte = hash.ComputeHash(Encoding.Unicode.GetBytes("contrasenia"));

                    encriptarArchivoClavePropia.CriptografiaFicheros(
                        path: archivoAES_TXT_Propia,
                        modo: CifrarDescifrar.cifrar,
                        keyParameter: keyHashByte,
                        iVparameter: encriptarArchivoClavePropia.IV
                    );
                    File.WriteAllBytes(archivoIVPropia, encriptarArchivoClavePropia.IV);
                    //---------------------------------------------------------


                    for (int x = 0; x < File.ReadAllBytes(archivoIVPropia).Length && x < encriptarArchivoClavePropia.IV.Length; x++)
                        Assert.IsTrue(File.ReadAllBytes(archivoIVPropia)[x] == encriptarArchivoClavePropia.IV[x]);

                    Assert.IsTrue(
                        encriptarArchivoClavePropia.Key == keyHashByte &&
                        File.Exists($"{archivoAES_TXT_Propia}.crypt") &&
                        Encoding.Unicode.GetString(File.ReadAllBytes($"{archivoAES_TXT_Propia}.crypt")) != _TEXTOPLANO
                    );
                }
            } finally {
                File.Delete(archivoAES_TXT);
                File.Delete(archivoAES_TXT_Propia);
            }
        }

        [TestMethod]
        public void DecifradoArchivos() {
            string archivoAES_TXT = "archivo.txt"; string archivoAES_TXT_Propia = "archivoPropia.txt";
            string archivoKeyRandom = "KeyArchivos.aes"; string archivoIVRandom = "IVarchivos.aes";
            string archivoIVPropia = "IVarchivosPropia.aes";
            CifradoArchivos();
            try {
                var desencriptarFicheroClaveRandom = new CifradoAES();
                desencriptarFicheroClaveRandom.CriptografiaFicheros(
                    path: archivoAES_TXT,
                    modo: CifrarDescifrar.descifrar,
                    keyParameter: File.ReadAllBytes(archivoKeyRandom),
                    iVparameter: File.ReadAllBytes(archivoIVRandom)
                );

                File.Delete(archivoKeyRandom);
                File.Delete(archivoIVRandom);
                File.Delete($"{archivoAES_TXT}.crypt");

                Assert.IsTrue(
                    !File.Exists(archivoKeyRandom) &&
                    !File.Exists(archivoIVRandom) &&
                    !File.Exists($"{archivoAES_TXT}.crypt") &&
                    File.ReadAllText(archivoAES_TXT) == _TEXTOPLANO
                );
                //----------------------CON CLAVE PROPIA---------------------\\
                var desencriptarArchivoClavePropia = new CifradoAES();
                using (HashAlgorithm hash = SHA256.Create()) {
                    byte[] keyHashByte = hash.ComputeHash(Encoding.Unicode.GetBytes("contrasenia"));

                    desencriptarArchivoClavePropia.CriptografiaFicheros(
                        path: $"{archivoAES_TXT_Propia}.crypt",
                        modo: CifrarDescifrar.descifrar,
                        keyParameter: keyHashByte,
                        iVparameter: File.ReadAllBytes(archivoIVPropia)
                    );
                }
                File.Delete(archivoIVPropia);
                File.Delete($"{archivoAES_TXT_Propia}.crypt");

                Assert.IsTrue(
                    !File.Exists(archivoIVPropia) &&
                    !File.Exists($"{archivoAES_TXT_Propia}.crypt") &&
                    File.ReadAllText(archivoAES_TXT_Propia) == _TEXTOPLANO
                );
            } finally {
                File.Delete(archivoAES_TXT);
                File.Delete(archivoAES_TXT_Propia);
            }
        }
    }
}
