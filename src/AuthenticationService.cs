using System;
using System.Security.Cryptography;
using System.Text;

namespace SafeVault
{
    public class AuthenticationService
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            string hash = HashPassword(password);

            return hash == storedHash;
        }

        public bool Login(string username, string password)
        {
            string storedPasswordHash =
                HashPassword("SecurePassword123");

            return username == "admin"
                && VerifyPassword(password, storedPasswordHash);
        }
    }
}