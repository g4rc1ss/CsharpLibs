using System;
using System.Security.Cryptography;
using System.Text;
using Garciss.Core.Libs.Encriptacion.AES.Cryptography.Clases;

namespace Garciss.Core.Libs.Encriptacion.AES.Cryptography {
    /// <summary>
    /// Clase con metodos y atributos para facilitar el uso de la clase 
    /// [System.Security.Cryptography].Aes
    /// </summary>
    public sealed class AESHelper {

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
        public byte[] IV { get; private set; }

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
        /// byte[] textoCifrado = cifrarTextoClaveRandom.EncriptarTexto(TEXTOPLANO, cifrarTextoClaveRandom.Key, cifrarTextoClaveRandom.IV);
        /// </code>
        /// </example>
        public byte[] EncriptarTexto(string text, byte[] keyParameter, byte[] iVparameter) {
            ValidarCampos(text, keyParameter, iVparameter);
            return EncryptAESHelper.EncryptStringToBytesAes(text, keyParameter, iVparameter);
        }

        /// <summary>
        /// Metodo para cifrar archivos
        /// </summary>
        /// <returns>
        /// Devuelve true o false dependiendo de si ha salido bien la operacion de
        /// cifrado
        /// </returns>
        /// <param name="originPath">ruta de archivo a cifrar</param>
        /// <param name="finalPath">Ruta donde se va a almacenar el archivo cifrado</param>
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
        ///         originPath: archivoAES_TXT_Propia,
        ///         finalPath: archivoCifradoAES
        ///         keyParameter: encriptarArchivoClavePropia.Key,
        ///         iVparameter: encriptarArchivoClavePropia.IV
        ///     );
        /// }           
        /// </code>
        /// </example>
        public bool EncriptarFichero(string originPath, string finalPath, byte[] keyParameter, byte[] iVparameter) {
            ValidarCampos(originPath, keyParameter, iVparameter);
            return EncryptAESHelper.EncryptFile(originPath, finalPath, keyParameter, iVparameter);
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
            ValidarCampos(cipherText, keyParameter, iVparameter);
            return DecryptAESHelper.DecryptStringFromBytesAes(cipherText, keyParameter, iVparameter);
        }

        /// <summary>
        /// Metodo para descifrar archivos
        /// </summary>
        /// <returns>
        /// Devuelve true o false dependiendo de si ha salido bien la operacion de
        /// descifrado
        /// </returns>
        /// <param name="originPath">ruta de archivo a descifrar</param>
        /// <param name="originPath">Ruta donde almacenar el archivo descifrado</param>
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
        ///         originPath: archivoAES_TXT_Propia,
        ///         finalPath: archivoDescifrado_TXT,
        ///         keyParameter: encriptarArchivoClavePropia.Key,
        ///         iVparameter: encriptarArchivoClavePropia.IV
        ///     );
        /// }           
        /// </code>
        /// </example>
        public bool DesencriptarFichero(string originPath, string finalPath, byte[] keyParameter, byte[] iVparameter) {
            ValidarCampos(originPath, keyParameter, iVparameter);
            return DecryptAESHelper.DecryptFile(originPath, finalPath, keyParameter, iVparameter);
        }

        /// <summary>
        /// Creamos las clasves para realizar el cifrado
        /// Cuando se creen estas claves e IV se guardaran en las propiedades de esta misma clase.
        /// </summary>
        /// <returns>
        /// Devuelve un true o false indicando si ha funcionado la creacion de claves
        /// </returns>
        /// <exception cref="CryptographicException"/>
        /// <exception cref="System.Reflection.TargetInvocationException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ObjectDisposedException"/>
        public bool CreateKeyIV(string clave) {
            using (var crear = Aes.Create()) {
                crear.KeySize = 256;
                using (HashAlgorithm hash = SHA256.Create()) {
                    Key = hash.ComputeHash(Encoding.Unicode.GetBytes(clave));
                }
                IV = crear.IV;
            }
            return true;
        }

        private static void ValidarCampos(params object[] campos) {
            foreach (var field in campos) {
                if (field is null) {
                    throw new ArgumentNullException(nameof(field), "Uno de los parametros enviados al metodo es null");
                }
            }
        }
    }
}
