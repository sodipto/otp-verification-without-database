using System.Security.Cryptography;
using System.Text;

namespace otp_verify_without_database.Utils
{
    public static class HashHelper
    {
        public static string CreateHMACSHA256Hash(string data, string secret)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            HMACSHA256 cryptographer = new HMACSHA256(encoding.GetBytes(secret));
            byte[] bytes = cryptographer.ComputeHash(encoding.GetBytes(data));

            string hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();

            return hash;
        }
    }
}
