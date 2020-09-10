using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Utils
{
    public static class RSAUtils
    {
        public static Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        /// <summary>
        /// 获取加密所使用的key，RSA算法是一种非对称密码算法，所谓非对称，就是指该算法需要一对密钥，使用其中一个加密，则需要用另一个才能解密。
        /// </summary>
        public static string GetKey()
        {
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            var PublicKey = rSACryptoServiceProvider.ToXmlString(false); // 获取公匙，用于加密
            var PrivateKey = rSACryptoServiceProvider.ToXmlString(true); // 获取公匙和私匙，用于解密
            keyValuePairs.Add(PublicKey, PrivateKey);
            return PublicKey;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <returns></returns>
        public static byte[] Encryption(string str, string publicKey)
        {
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            rSACryptoServiceProvider.FromXmlString(publicKey);
            byte[] buffer = Encoding.UTF8.GetBytes(str); // 将明文转换为byte[]

            // 加密后的数据就是一个byte[] 数组,可以以 文件的形式保存 或 别的形式(网上很多教程,使用Base64进行编码化保存)
            byte[] EncryptBuffer = rSACryptoServiceProvider.Encrypt(buffer, false); // 进行加密

            //string EncryptBase64 = Convert.ToBase64String(EncryptBuffer); // 如果使用base64进行明文化，在解密时 需要再次将base64 转换为byte[]
            //Console.WriteLine(EncryptBase64);
            return EncryptBuffer;
        }

        public static string Encryption2(string str, string publickKey)
        {
            var EncryptBase64 = Convert.ToBase64String(Encryption(str, publickKey));
            return EncryptBase64;
        }

        public static string Decrypt(byte[] buffer, string publicKey)
        {
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider();
            rSACryptoServiceProvider.FromXmlString(keyValuePairs[publicKey]);
            // 解密后得到一个byte[] 数组
            byte[] DecryptBuffer = rSACryptoServiceProvider.Decrypt(buffer, false); // 进行解密
            string str = Encoding.UTF8.GetString(DecryptBuffer); // 将byte[]转换为明文

            return str;
        }

    }
}
