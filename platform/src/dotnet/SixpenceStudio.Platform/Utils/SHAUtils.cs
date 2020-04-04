using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Utils
{
    public class SHAUtils
    {
        public static string SHA256Encrypt(string StrIn)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(StrIn);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return BitConverter.ToString(Result).Replace("-", "").ToLower(); // 返回 64
        }
}
}
