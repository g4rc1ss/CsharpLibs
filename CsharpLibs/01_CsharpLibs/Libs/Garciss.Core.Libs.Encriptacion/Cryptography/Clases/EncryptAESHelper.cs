using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Garciss.Core.Libs.Encriptacion.Cryptography.Clases {
    internal sealed class EncryptAESHelper {

        internal byte[] EncryptStringToBytesAes(string text, byte[] keyParameter, byte[] iVparameter) {
            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create()) {
                aesAlg.Key = keyParameter;
                aesAlg.IV = iVparameter;
                // Create an encryptor to perform the stream transform.
                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))                     // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                    using (var swEncrypt = new StreamWriter(csEncrypt)) {
                        //Write all data to the stream.
                        swEncrypt.Write(text);
                        text = string.Empty;
                    }
                    return msEncrypt.ToArray();
                }
            }
            // Return the encrypted bytes from the memory stream.
        }

        internal bool EncryptFile(string pathFileToEncrypt, string pathEncryptedFile, byte[] keyParameter, byte[] iVparameter) {
            try {
                using (var aesAlg = Aes.Create()) {
                    aesAlg.Key = keyParameter;
                    aesAlg.IV = iVparameter;

                    using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                    using (var fileStreamOutput = new FileStream(pathEncryptedFile, FileMode.OpenOrCreate, FileAccess.Write))
                    using (var cryptStream = new CryptoStream(fileStreamOutput, encryptor, CryptoStreamMode.Write))
                    using (var fileStreamInput = new FileStream(pathFileToEncrypt, FileMode.Open, FileAccess.Read))
                        for (int data; (data = fileStreamInput.ReadByte()) != -1;)
                            cryptStream.WriteByte((byte)data);
                }
                return true;
            } catch (Exception) {
                return false;
            }
        }

    }
}
