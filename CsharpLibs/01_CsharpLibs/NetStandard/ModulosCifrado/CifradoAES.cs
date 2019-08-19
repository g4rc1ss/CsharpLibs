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
        /// <param name="Text">Texto a encriptar</param>
        /// <param name="KeyParameter">
        /// Contraseña de un maximo de tamaño de 256, la contraseña
        /// se tiene que cifrar con un hash como MD5, SHA256
        /// </param>
        /// <param name="IVparameter">
        /// Vector de Inicializacion, es un bloque de bits obligatorio en los algoritmos
        /// de cifrado por bloque. https://es.wikipedia.org/wiki/Vector_de_inicialización
        /// </param>
        public byte[] EncriptarTexto(string Text, byte[] KeyParameter = null, byte[] IVparameter = null) {
            return EncryptStringToBytes_Aes(Text: Text, KeyParameter: KeyParameter, IVparameter: IVparameter);
        }

        private byte[] EncryptStringToBytes_Aes(string Text, byte[] KeyParameter = null, byte[] IVparameter = null) {
            // Check arguments.
            if (KeyParameter == null && IVparameter == null) {
                if (!Create())
                    throw new ArgumentException("Ha fallado la generacion de claves aleatoria");
            } else {
                Key = KeyParameter;
                IV = IVparameter;
            }
            if (string.IsNullOrEmpty(Text))
                throw new ArgumentNullException("plainText");
            if ((KeyParameter == null || KeyParameter.Length <= 0) && (Key == null || Key.Length <= 0))
                throw new ArgumentNullException("Key");
            if ((IVparameter == null || IVparameter.Length <= 0) && (IV == null || IV.Length <= 0))
                throw new ArgumentNullException("IV");
            //----------------------------------------------------------------------------------------\\
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create()) {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                // Create an encryptor to perform the stream transform.
                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)) {
                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream()) {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                                //Write all data to the stream.
                                swEncrypt.Write(Text);
                                Text = string.Empty;
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
        /// <param name="KeyParameter">
        /// Contraseña de un maximo de tamaño de 256, la contraseña
        /// se tiene que cifrar con un hash como MD5, SHA256
        /// </param>
        /// <param name="IVparameter">
        /// Vector de Inicializacion, es un bloque de bits obligatorio en los algoritmos
        /// de cifrado por bloque. https://es.wikipedia.org/wiki/Vector_de_inicialización
        /// </param>
        public string DesencriptarTexto(byte[] cipherText, byte[] KeyParameter = null, byte[] IVparameter = null) {
            return DecryptStringFromBytes_Aes(cipherText: cipherText, KeyParameter: KeyParameter, IVparameter: IVparameter);
        }

        private string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] KeyParameter = null, byte[] IVparameter = null) {
            if (KeyParameter != null && IVparameter != null) {
                Key = KeyParameter;
                IV = IVparameter;
            }
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if ((KeyParameter == null || KeyParameter.Length <= 0) && (Key == null || Key.Length <= 0))
                throw new ArgumentNullException("Key");
            if ((IVparameter == null || IVparameter.Length <= 0) && (IV == null || IV.Length <= 0))
                throw new ArgumentNullException("IV");

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create()) {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)) {
                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(cipherText)) {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt)) {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
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
        /// <param name="Path">ruta de archivo a cifrar</param>
        /// <param name="modo">enum para elegir si queremos cifrar o descifrar</param>
        /// <param name="KeyParameter">
        /// Contraseña de un maximo de tamaño de 256, la contraseña
        /// se tiene que cifrar con un hash como MD5, SHA256
        /// </param>
        /// <param name="IVparameter">
        /// Vector de Inicializacion, es un bloque de bits obligatorio en los algoritmos
        /// de cifrado por bloque. https://es.wikipedia.org/wiki/Vector_de_inicialización
        /// </param>
        public bool CriptografiaFicheros(string Path, cifrarDescifrar modo, byte[] KeyParameter = null, byte[] IVparameter = null) {
            return CryptDecrypt_File(Path: Path, modo: modo, KeyParameter: KeyParameter, IVparameter: IVparameter);
        }
        private bool CryptDecrypt_File(string Path, cifrarDescifrar modo, byte[] KeyParameter = null, byte[] IVparameter = null) {
            // Check arguments.
            if (KeyParameter == null && IVparameter == null && modo == cifrarDescifrar.cifrar) {
                if (!Create())
                    throw new ArgumentException("Ha fallado la generacion de claves aleatoria");
            } else {
                Key = KeyParameter;
                IV = IVparameter;
            }
            if (string.IsNullOrEmpty(Path))
                throw new ArgumentNullException("No hay ruta");
            if ((KeyParameter == null || KeyParameter.Length <= 0) && (Key == null || Key.Length <= 0))
                throw new ArgumentNullException("Key");
            if ((IVparameter == null || IVparameter.Length <= 0) && (IV == null || IV.Length <= 0))
                throw new ArgumentNullException("IV");
            //----------------------------------------------------------------------------------------\\
            // Create an Aes object
            // with the specified key and IV.
            try {
                using (Aes aesAlg = Aes.Create()) {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    // Create the streams used for encryption.
                    if (modo == cifrarDescifrar.cifrar) {
                        // Create an encryptor to perform the stream transform.
                        using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)) {
                            using (FileStream fileStreamOutput = new FileStream($"{Path}.crypt", FileMode.Create, FileAccess.Write)) {
                                using (CryptoStream cryptStream = new CryptoStream(fileStreamOutput, encryptor, CryptoStreamMode.Write)) {
                                    using (FileStream fileStreamInput = new FileStream(Path, FileMode.Open, FileAccess.Read)) {
                                        for (int data; (data = fileStreamInput.ReadByte()) != -1;)
                                            cryptStream.WriteByte((byte)data);

                                        fileStreamInput.Close();
                                    }
                                    cryptStream.Close();
                                }
                                fileStreamOutput.Close();
                            }
                        }
                    }
                    if (modo == cifrarDescifrar.descifrar) {
                        // Create an encryptor to perform the stream transform.
                        using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)) {
                            using (FileStream fileStreamCrypt = new FileStream(Path.Contains(".crypt") ? Path : $"{Path}.crypt", FileMode.Open, FileAccess.Read)) {
                                using (FileStream fileStreamOut = new FileStream(Path.Contains(".crypt") ? Path.Replace(".crypt", "") : $"{Path}", FileMode.Create, FileAccess.Write)) {
                                    using (CryptoStream decryptStream = new CryptoStream(fileStreamCrypt, decryptor, CryptoStreamMode.Read)) {
                                        for (int data; (data = decryptStream.ReadByte()) != -1;)
                                            fileStreamOut.WriteByte((byte)data);

                                        decryptStream.Close();
                                    }
                                    fileStreamOut.Close();
                                }
                                fileStreamCrypt.Close();
                            }
                        }
                    }
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
                using (Aes crear = Aes.Create()) {
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
    public enum cifrarDescifrar {
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
