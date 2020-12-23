using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Garciss.Core.Libs.Encriptacion.Cryptography.Clases {
    internal class DecryptAESHelper {
        internal bool DecryptFile(string path, byte[] keyParameter, byte[] iVparameter) {
            // Create an Aes object
            // with the specified key and IV.
            try {
                using (var aesAlg = Aes.Create()) {
                    aesAlg.Key = keyParameter;
                    aesAlg.IV = iVparameter;

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

        internal string DecryptStringFromBytesAes(byte[] cipherText, byte[] keyParameter = null, byte[] iVparameter = null) {
            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create()) {
                aesAlg.Key = keyParameter;
                aesAlg.IV = iVparameter;

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

    }
}
