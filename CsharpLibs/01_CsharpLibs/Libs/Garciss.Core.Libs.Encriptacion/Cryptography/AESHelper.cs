using Garciss.Core.Libs.Encriptacion.Cryptography.Clases;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Garciss.Core.Libs.Encriptacion.Cryptography {
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
            ValidarCampos(text, keyParameter, iVparameter);
            return new EncryptAESHelper().EncryptStringToBytesAes(text: text, keyParameter: Key, iVparameter: IV);
        }

        /// <summary>
        /// Metodo para cifrar archivos
        /// </summary>
        /// <returns>
        /// Devuelve true o false dependiendo de si ha salido bien la operacion de
        /// cifrado
        /// </returns>
        /// <param name="path">ruta de archivo a cifrar</param>
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
        ///     encriptarArchivoClavePropia.EncriptarFichero(
        ///         path: archivoAES_TXT_Propia,
        ///         keyParameter: keyHashByte,
        ///         iVparameter: encriptarArchivoClavePropia.IV
        ///     );
        /// }           
        /// </code>
        /// </example>
        public bool EncriptarFichero(string path, byte[] keyParameter = null, byte[] iVparameter = null) {
            ValidarCampos(path, keyParameter, iVparameter);
            return new EncryptAESHelper().EncryptFile(path: path, keyParameter: Key, iVparameter: IV);
        }

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
        public string DesencriptarTexto(byte[] cipherText, byte[] keyParameter, byte[] iVparameter) {
            ValidarCampos(cipherText.ToString(), keyParameter, iVparameter);
            return new DecryptAESHelper().DecryptStringFromBytesAes(cipherText: cipherText, keyParameter: keyParameter, iVparameter: iVparameter);
        }

        /// <summary>
        /// Metodo para descifrar archivos
        /// </summary>
        /// <returns>
        /// Devuelve true o false dependiendo de si ha salido bien la operacion de
        /// descifrado
        /// </returns>
        /// <param name="path">ruta de archivo a descifrar</param>
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
        ///     encriptarArchivoClavePropia.DesenciptarFichero(
        ///         path: archivoAES_TXT_Propia,
        ///         keyParameter: keyHashByte,
        ///         iVparameter: encriptarArchivoClavePropia.IV
        ///     );
        /// }           
        /// </code>
        /// </example>
        public bool DesencriptarFichero(string path, byte[] keyParameter = null, byte[] iVparameter = null) {
            ValidarCampos(path, keyParameter, iVparameter);
            return new DecryptAESHelper().DecryptFile(path: path, keyParameter: Key, iVparameter: IV);
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

        private void ValidarCampos(string text, byte[] keyParameter, byte[] iVparameter) {
            if (keyParameter == null && iVparameter == null) {
                if (!Create()) {
                    throw new ArgumentException("Ha fallado la generacion de claves aleatoria");
                }
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
        }
    }
}
