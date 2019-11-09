using System;
using System.IO;
using System.Security.Cryptography;

namespace Core.Libs.Encriptacion.Cryptography {
    /// <summary>
    /// Clase con metodos y atributos para facilitar el uso de la clase 
    /// [System.Security.Cryptography].Aes
    /// </summary>
    public class AESHelper {

        /// <summary>
        /// Un array de bytes que contiene la contraseña con la que se encriptara
        /// el texto o descifrara
        /// </summary>
        /// <returns>
        /// Devuelve la contraseña aleatoria generada, la condicion para obtenerla es cifrar
        /// el texto
        /// </returns>
        public byte[] Key { get; private set; }

        /// <summary>
        /// Vector de Inicializacion, es un bloque de bits obligatorio en los algoritmos
        /// de cifrado por bloque. https://es.wikipedia.org/wiki/Vector_de_inicialización
        /// </summary>
        /// <returns>
        /// Devuelve el vector de inicializacion
        /// </returns>
        public byte[] IV { get; private set; } = Aes.Create().IV;
        #region Encriptar

        /// <summary>
        /// Metodo para cifrar una cadena en el algoritmo AES
        /// </summary>
        /// <returns>
        /// Devuelve un array de byte[] con el texto encriptado
        /// </returns>
        /// <param name="text">Texto a encriptar</param>
        /// <param name="keyParameter">
        /// Contraseña de un maximo de tamaño de 256, la contraseña
        /// se tiene que cifrar con un hash como MD5, SHA256
        /// </param>
        /// <param name="iVparameter">
        /// Vector de Inicializacion, es un bloque de bits obligatorio en los algoritmos
        /// de cifrado por bloque. https://es.wikipedia.org/wiki/Vector_de_inicialización
        /// </param>
        /// <example>
        /// <code>
        /// byte[] textoCifrado = cifrarTextoClaveRandom.EncriptarTexto(TEXTOPLANO);
        /// </code>
        /// </example>
        public byte[] EncriptarTexto(string text, byte[] keyParameter = null, byte[] iVparameter = null) {
            return EncryptStringToBytes_Aes(text: text, keyParameter: keyParameter, iVparameter: iVparameter);
        }

        private byte[] EncryptStringToBytes_Aes(string text, byte[] keyParameter = null, byte[] iVparameter = null) {
            // Check arguments.
            if (keyParameter == null && iVparameter == null) {
                if (!Create())
                    throw new ArgumentException("Ha fallado la generacion de claves aleatoria");
            } else {
                Key = keyParameter;
                IV = iVparameter;
            }
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("plainText");
            if ((keyParameter == null || keyParameter.Length <= 0) && (Key == null || Key.Length <= 0))
                throw new ArgumentNullException("Key");
            if ((iVparameter == null || iVparameter.Length <= 0) && (IV == null || IV.Length <= 0))
                throw new ArgumentNullException("IV");
            //----------------------------------------------------------------------------------------\\
            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create()) {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                // Create an encryptor to perform the stream transform.
                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)) {
                    // Create the streams used for encryption.
                    using (var msEncrypt = new MemoryStream()) {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                            using (var swEncrypt = new StreamWriter(csEncrypt)) {
                                //Write all data to the stream.
                                swEncrypt.Write(text);
                                text = string.Empty;
                            }
                            return (msEncrypt.ToArray());
                        }
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
        }
        #endregion

        #region Desencriptar
        /// <summary>
        /// Metodo para descifrar una array de bytes cifrados en algoritmo AES
        /// </summary>
        /// <returns>
        /// Devuelve una cadena(string) con el texto descifrado
        /// </returns>
        /// <param name="cipherText">conjunto de bytes cifrados</param>
        /// <param name="keyParameter">
        /// Contraseña de un maximo de tamaño de 256, la contraseña
        /// se tiene que cifrar con un hash como MD5, SHA256
        /// </param>
        /// <param name="iVparameter">
        /// Vector de Inicializacion, es un bloque de bits obligatorio en los algoritmos
        /// de cifrado por bloque. https://es.wikipedia.org/wiki/Vector_de_inicialización
        /// </param>
        /// <example>
        /// <code>
        /// string textoDescifradoPropio = descifrarTextoClavePropia.DesencriptarTexto(
        ///     cipherText: Aqui el texto,
        ///     keyParameter: Aqui la Key,
        ///     iVparameter: Aqui el IV
        /// );
        /// </code>
        /// </example>
        public string DesencriptarTexto(byte[] cipherText, byte[] keyParameter = null, byte[] iVparameter = null) {
            return DecryptStringFromBytes_Aes(cipherText: cipherText, keyParameter: keyParameter, iVparameter: iVparameter);
        }

        private string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] keyParameter = null, byte[] iVparameter = null) {
            if (keyParameter != null && iVparameter != null) {
                Key = keyParameter;
                IV = iVparameter;
            }
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if ((keyParameter == null || keyParameter.Length <= 0) && (Key == null || Key.Length <= 0))
                throw new ArgumentNullException("Key");
            if ((iVparameter == null || iVparameter.Length <= 0) && (IV == null || IV.Length <= 0))
                throw new ArgumentNullException("IV");

            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create()) {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                    // Read the decrypted bytes from the decrypting stream
                    // and place them in a string.
                    return srDecrypt.ReadToEnd();
            }
        }
        #endregion
        /// <summary>
        /// Metodo para cifrar y descifrar archivos
        /// </summary>
        /// <returns>
        /// Devuelve true o false dependiendo de si ha salido bien la operacion de
        /// cifrado o descifrado
        /// </returns>
        /// <param name="path">ruta de archivo a cifrar</param>
        /// <param name="modo">enum para elegir si queremos cifrar o descifrar</param>
        /// <param name="keyParameter">
        /// Contraseña de un maximo de tamaño de 256, la contraseña
        /// se tiene que cifrar con un hash como MD5, SHA256
        /// </param>
        /// <param name="iVparameter">
        /// Vector de Inicializacion, es un bloque de bits obligatorio en los algoritmos
        /// de cifrado por bloque. https://es.wikipedia.org/wiki/Vector_de_inicialización
        /// </param>
        /// <example>
        /// <code>
        /// var encriptarArchivoClavePropia = new AES();
        /// using (HashAlgorithm hash = SHA256.Create()) {
        ///     byte[] keyHashByte = hash.ComputeHash(Encoding.Unicode.GetBytes("contrasenia"));
        ///     encriptarArchivoClavePropia.CriptografiaFicheros(
        ///         path: archivoAES_TXT_Propia,
        ///         modo: CifrarDescifrar.cifrar,
        ///         keyParameter: keyHashByte,
        ///         iVparameter: encriptarArchivoClavePropia.IV
        ///     );
        /// }           
        /// </code>
        /// </example>
        public bool CriptografiaFicheros(string path, CifrarDescifrar modo, byte[] keyParameter = null, byte[] iVparameter = null) {
            return CryptDecrypt_File(path: path, modo: modo, keyParameter: keyParameter, iVparameter: iVparameter);
        }

        private bool CryptDecrypt_File(string path, CifrarDescifrar modo, byte[] keyParameter = null, byte[] iVparameter = null) {
            // Check arguments.
            if (keyParameter == null && iVparameter == null && modo == CifrarDescifrar.cifrar) {
                if (!Create())
                    throw new ArgumentException("Ha fallado la generacion de claves aleatoria");
            } else {
                Key = keyParameter;
                IV = iVparameter;
            }
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("No hay ruta");
            if ((keyParameter == null || keyParameter.Length <= 0) && (Key == null || Key.Length <= 0))
                throw new ArgumentNullException("Key");
            if ((iVparameter == null || iVparameter.Length <= 0) && (IV == null || IV.Length <= 0))
                throw new ArgumentNullException("IV");
            //----------------------------------------------------------------------------------------\\
            // Create an Aes object
            // with the specified key and IV.
            try {
                using (var aesAlg = Aes.Create()) {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    // Create the streams used for encryption.
                    if (modo == CifrarDescifrar.cifrar)
                        // Create an encryptor to perform the stream transform.
                        using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                        using (var fileStreamOutput = new FileStream($"{path}.crypt", FileMode.Create, FileAccess.Write))
                        using (var cryptStream = new CryptoStream(fileStreamOutput, encryptor, CryptoStreamMode.Write))
                        using (var fileStreamInput = new FileStream(path, FileMode.Open, FileAccess.Read))
                            for (int data; (data = fileStreamInput.ReadByte()) != -1;)
                                cryptStream.WriteByte((byte)data);

                    else if (modo == CifrarDescifrar.descifrar)
                        // Create an encryptor to perform the stream transform.
                        using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                        using (var fileStreamCrypt = new FileStream(path.Contains(".crypt") ? path : $"{path}.crypt", FileMode.Open, FileAccess.Read))
                        using (var fileStreamOut = new FileStream(path.Contains(".crypt") ? path.Replace(".crypt", "") : $"{path}", FileMode.Create, FileAccess.Write))
                        using (var decryptStream = new CryptoStream(fileStreamCrypt, decryptor, CryptoStreamMode.Read))
                            for (int data; (data = decryptStream.ReadByte()) != -1;)
                                fileStreamOut.WriteByte((byte)data);
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }

        /// <summary>
        /// Creamos claves aleatorias de cifrado si no queremos usar una contraseña propia
        /// </summary>
        /// <returns>
        /// devuelve un true o false indicando si ha funcionado la creacion de claves
        /// </returns>
        /// <exception cref="CryptographicException"/>
        /// <exception cref="System.Reflection.TargetInvocationException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ObjectDisposedException"/>
        private bool Create() {
            try {
                using (var crear = Aes.Create()) {
                    crear.KeySize = 256;
                    using (HashAlgorithm hash = SHA256.Create())
                        Key = hash.ComputeHash(crear.Key);
                    IV = crear.IV;
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }

    /// <summary>
    /// Enumerador para indicar si queremos cifrar o descifrar
    /// </summary>
    public enum CifrarDescifrar {
        /// <summary>
        /// indicador de que quieres cifrar algo, por ejemplo un documento
        /// </summary>
        /// <example>cifrarDescifrar.cifrar</example>
        cifrar = 0,
        /// <summary>
        /// indicador de que quieres descifrar algo, por ejemplo un archivo zip
        /// </summary>
        /// <example>cifrarDescifrar.descifrar</example>
        descifrar = 1
    }
}
