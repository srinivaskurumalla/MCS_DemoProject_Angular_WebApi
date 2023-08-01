using System.Security.Cryptography;


namespace MCS_DemoProject_Angular_WebApi.Repositories
{
    public class JwtKeyGenerator
    {
        public static string Generate256BitKey()
        {
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var keyBytes = new byte[32]; // 256 bits = 32 bytes
                randomNumberGenerator.GetBytes(keyBytes);
                return Convert.ToBase64String(keyBytes);
            }
        }
    }
}
