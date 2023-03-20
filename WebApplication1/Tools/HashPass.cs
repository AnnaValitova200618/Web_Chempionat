using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Tools
{
    public static class HashPass
    {
        public static string GetHash(string pass)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pass);
            byte[] hashBytes = MD5.Create().ComputeHash(bytes);
            return Encoding.UTF8.GetString(hashBytes);
        }
    }
}
