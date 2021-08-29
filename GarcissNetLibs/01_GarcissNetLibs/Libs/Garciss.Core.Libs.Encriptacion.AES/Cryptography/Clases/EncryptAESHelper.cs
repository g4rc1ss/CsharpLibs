using System.IO;
using System.Security.Cryptography;

namespace Garciss.Core.Libs.Encriptacion.AES.Cryptography.Clases {
    internal sealed class EncryptAESHelper {

        internal static byte[] EncryptStringToBytesAes(string text, byte[] keyParameter, byte[] iVparameter) {
            using var aesAlg = Aes.Create();
            using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using var swEncrypt = new StreamWriter(csEncrypt);

            aesAlg.Key = keyParameter;
            aesAlg.IV = iVparameter;

            swEncrypt.Write(text);

            return msEncrypt.ToArray();
        }

        internal static bool EncryptFile(string pathFileToEncrypt, string pathEncryptedFile, byte[] keyParameter, byte[] iVparameter) {
            using var aesAlg = Aes.Create();
            using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using var fileStreamOutput = new FileStream(pathEncryptedFile, FileMode.OpenOrCreate, FileAccess.Write);
            using var cryptStream = new CryptoStream(fileStreamOutput, encryptor, CryptoStreamMode.Write);
            using var fileStreamInput = new FileStream(pathFileToEncrypt, FileMode.Open, FileAccess.Read);

            aesAlg.Key = keyParameter;
            aesAlg.IV = iVparameter;

            for (int data; (data = fileStreamInput.ReadByte()) != -1;) {
                cryptStream.WriteByte((byte)data);
            }
            return true;
        }

    }
}
