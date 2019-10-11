using System;
using System.IO;
using System.Security.Cryptography;

namespace ModulosCifrado {
    /// <summary>
    /// Clase con metodos y atributos para facilitar el uso de la clase 
    /// [System.Security.Cryptography].Aes
    /// </summary>
    public class CifradoAES {

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
        public byte[] EncriptarTexto(string text, byte[] keyParameter = null, byte[] iVparameter = null) =>
            EncryptStringToBytes_Aes(_text: text, _keyParameter: keyParameter, _iVparameter: iVparameter);

        private byte[] EncryptStringToBytes_Aes(string _text, byte[] _keyParameter = null, byte[] _iVparameter = null) {
            // Check arguments.
            if (_keyParameter == null && _iVparameter == null) {
                if (!Create())
                    throw new ArgumentException("Ha fallado la generacion de claves aleatoria");
            } else {
                Key = _keyParameter;
                IV = _iVparameter;
            }
            if (string.IsNullOrEmpty(_text))
                throw new ArgumentNullException("plainText");
            if ((_keyParameter == null || _keyParameter.Length <= 0) && (Key == null || Key.Length <= 0))
                throw new ArgumentNullException("Key");
            if ((_iVparameter == null || _iVparameter.Length <= 0) && (IV == null || IV.Length <= 0))
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
                                swEncrypt.Write(_text);
                                _text = string.Empty;
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
        public string DesencriptarTexto(byte[] cipherText, byte[] keyParameter = null, byte[] iVparameter = null) =>
            DecryptStringFromBytes_Aes(_cipherText: cipherText, _keyParameter: keyParameter, _iVparameter: iVparameter);

        private string DecryptStringFromBytes_Aes(byte[] _cipherText, byte[] _keyParameter = null, byte[] _iVparameter = null) {
            if (_keyParameter != null && _iVparameter != null) {
                Key = _keyParameter;
                IV = _iVparameter;
            }
            // Check arguments.
            if (_cipherText == null || _cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if ((_keyParameter == null || _keyParameter.Length <= 0) && (Key == null || Key.Length <= 0))
                throw new ArgumentNullException("Key");
            if ((_iVparameter == null || _iVparameter.Length <= 0) && (IV == null || IV.Length <= 0))
                throw new ArgumentNullException("IV");

            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create()) {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(_cipherText))
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
        public bool CriptografiaFicheros(string path, CifrarDescifrar modo, byte[] keyParameter = null, byte[] iVparameter = null) =>
            CryptDecrypt_File(_path: path, _modo: modo, _keyParameter: keyParameter, _iVparameter: iVparameter);

        private bool CryptDecrypt_File(string _path, CifrarDescifrar _modo, byte[] _keyParameter = null, byte[] _iVparameter = null) {
            // Check arguments.
            if (_keyParameter == null && _iVparameter == null && _modo == CifrarDescifrar.cifrar) {
                if (!Create())
                    throw new ArgumentException("Ha fallado la generacion de claves aleatoria");
            } else {
                Key = _keyParameter;
                IV = _iVparameter;
            }
            if (string.IsNullOrEmpty(_path))
                throw new ArgumentNullException("No hay ruta");
            if ((_keyParameter == null || _keyParameter.Length <= 0) && (Key == null || Key.Length <= 0))
                throw new ArgumentNullException("Key");
            if ((_iVparameter == null || _iVparameter.Length <= 0) && (IV == null || IV.Length <= 0))
                throw new ArgumentNullException("IV");
            //----------------------------------------------------------------------------------------\\
            // Create an Aes object
            // with the specified key and IV.
            try {
                using (var aesAlg = Aes.Create()) {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    // Create the streams used for encryption.
                    if (_modo == CifrarDescifrar.cifrar)
                        // Create an encryptor to perform the stream transform.
                        using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                        using (var fileStreamOutput = new FileStream($"{_path}.crypt", FileMode.Create, FileAccess.Write))
                        using (var cryptStream = new CryptoStream(fileStreamOutput, encryptor, CryptoStreamMode.Write))
                        using (var fileStreamInput = new FileStream(_path, FileMode.Open, FileAccess.Read))
                            for (int data; (data = fileStreamInput.ReadByte()) != -1;)
                                cryptStream.WriteByte((byte)data);

                    else if (_modo == CifrarDescifrar.descifrar)
                        // Create an encryptor to perform the stream transform.
                        using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                        using (var fileStreamCrypt = new FileStream(_path.Contains(".crypt") ? _path : $"{_path}.crypt", FileMode.Open, FileAccess.Read))
                        using (var fileStreamOut = new FileStream(_path.Contains(".crypt") ? _path.Replace(".crypt", "") : $"{_path}", FileMode.Create, FileAccess.Write))
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
        descifrar = 1
    }
}
