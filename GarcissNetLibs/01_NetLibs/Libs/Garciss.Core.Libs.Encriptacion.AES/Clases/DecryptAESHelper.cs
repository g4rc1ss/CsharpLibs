using System.IO;
using System.Security.Cryptography;

namespace Garciss.Core.Libs.Encriptacion.AES.Clases {
    internal sealed class DecryptAESHelper {
        
        internal static string DecryptStringFromBytesAes(byte[] cipherText, byte[] keyParameter = null, byte[] iVparameter = null) {
            using (var aesAlg = Aes.Create()) {
                aesAlg.Key = keyParameter;
                aesAlg.IV = iVparameter;
                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                using (var msDecrypt = new MemoryStream(cipherText))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt)) {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        internal static bool DecryptFile(string cryptFilePath, string decryptFilePath, byte[] keyParameter, byte[] iVparameter) {
            using (var aesAlg = Aes.Create()) {
                aesAlg.Key = keyParameter;
                aesAlg.IV = iVparameter;
                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                using (var fileStreamCrypt = new FileStream(cryptFilePath, FileMode.Open, FileAccess.Read))
                using (var fileStreamOut = new FileStream(decryptFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                using (var decryptStream = new CryptoStream(fileStreamCrypt, decryptor, CryptoStreamMode.Read)) {
                    for (int data; (data = decryptStream.ReadByte()) != -1;) {
                        fileStreamOut.WriteByte((byte)data);
                    }
                }
            }
            return true;
        }

    }
}
