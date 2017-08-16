using BCrypt.Net;

namespace benandkatiegetmarried.Common.Security
{
    public static class SecurityExtensions
    {
        public static string EncryptPassword(this string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool CheckPassword(this string password, string text)
        {
            return BCrypt.Net.BCrypt.Verify(text, password);
        }
    }
}