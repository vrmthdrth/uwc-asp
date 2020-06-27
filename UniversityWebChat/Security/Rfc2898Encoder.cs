using System.Security.Cryptography;
using System.Text;

namespace UniversityWebChat.Security
{
    public class Rfc2898Encoder
    {
        private const int HASH_LENGTH = 160 / 8;                          
        private const int ITERATIONS_NUMBER = 1024;
        public static string Encode(string password, string salt)
        {
            return CreateHash(password, salt, ITERATIONS_NUMBER);
        }
        public static bool Validate(string enteredPassword, string encodedPassword, string salt)
        {
            string hashToCompare = Encode(enteredPassword, salt);
            return hashToCompare == encodedPassword;
        }
        private static string CreateHash(string password, string salt, int iterations)
        {
            byte[] hash;
            byte[] saltBytes = Encoding.Default.GetBytes(salt);                                         //utf8
            using (var hashGenerator = new Rfc2898DeriveBytes(password, saltBytes, iterations))
            {
                hash = hashGenerator.GetBytes(HASH_LENGTH);
            }

            return Encoding.Default.GetString(hash);                                                    //utf8
        }
    }
}
