using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SixpenceStudio.Platform.Utils
{
    public class DecryptAndEncryptHelper
    {
        // 对称算法基类
        private readonly SymmetricAlgorithm _symmetricAlgorithm;

        // Encryption
        private static string defaultKey = "01D8FE63-D3AF-4BE9-81DF-F6611719C550";
        private string _key;
        public string Key
        {
            get { return _key; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _key = value;
                }
                else
                {
                    _key = defaultKey;
                }
            }
        }

        // Decryption
        private static string defaultIV = "302B376B-A0EE-432C-B5DB-D2DCB9C7C38C";
        private string _iv = "";
        public string IV
        {
            get { return _iv; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _iv = value;
                }
                else
                {
                    _iv = defaultIV;
                }
            }
        }

        #region 构造函数
        public DecryptAndEncryptHelper()
        {
            _symmetricAlgorithm = new RijndaelManaged();
        }

        public DecryptAndEncryptHelper(string Key, string IV)
        {
            _symmetricAlgorithm = new RijndaelManaged();
            _key = string.IsNullOrEmpty(Key) ? defaultKey : Key;
            _iv = string.IsNullOrEmpty(IV) ? defaultIV : IV;
        }
        #endregion


        /// <summary>
        /// Get Key
        /// </summary>
        /// <returns>密钥</returns>
        private byte[] GetLegalKey()
        {
            _symmetricAlgorithm.GenerateKey();
            byte[] bytTemp = _symmetricAlgorithm.Key;
            int KeyLength = bytTemp.Length;
            if (_key.Length > KeyLength)
                _key = _key.Substring(0, KeyLength);
            else if (_key.Length < KeyLength)
                _key = _key.PadRight(KeyLength, '#');
            return Encoding.ASCII.GetBytes(_key);
        }

        /// <summary>
        /// Get IV
        /// </summary>
        private byte[] GetLegalIV()
        {
            _symmetricAlgorithm.GenerateIV();
            byte[] bytTemp = _symmetricAlgorithm.IV;
            int IVLength = bytTemp.Length;
            if (_iv.Length > IVLength)
                _iv = _iv.Substring(0, IVLength);
            else if (_iv.Length < IVLength)
                _iv = _iv.PadRight(IVLength, '#');
            return Encoding.ASCII.GetBytes(_iv);
        }

        /// <summary>
        /// Encrypto 加密
        /// </summary>
        public string Encrypto(string Source)
        {
            byte[] bytIn = Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            _symmetricAlgorithm.Key = GetLegalKey();
            _symmetricAlgorithm.IV = GetLegalIV();
            ICryptoTransform encrypto = _symmetricAlgorithm.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }

        /// <summary>
        /// Decrypto 解密
        /// </summary>
        public string Decrypto(string Source)
        {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            _symmetricAlgorithm.Key = GetLegalKey();
            _symmetricAlgorithm.IV = GetLegalIV();
            ICryptoTransform encrypto = _symmetricAlgorithm.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            var result = sr.ReadToEnd();
            return result;
        }

    }
}
