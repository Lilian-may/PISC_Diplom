#nullable disable
using System;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace Pipe
{
    public static class SecurityHelper
    {
        public static string HashPassword(string password)
        {
            try
            {
                // BCrypt.Net-Next API
                return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
            }
            catch (Exception ex)
            {
                AuditLogger.LogError(Program.CurrentUser ?? "system", "HashPassword", ex);
                throw new InvalidOperationException("Ошибка хэширования пароля", ex);
            }
        }

        public static bool VerifyPassword(string password, string hash)
        {
            try
            {
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                    return false;
                // BCrypt.Net-Next API
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
            catch (Exception ex)
            {
                AuditLogger.LogError(Program.CurrentUser ?? "system", "VerifyPassword", ex);
                return false;
            }
        }

        public static string GenerateRandomPassword(int length = 12)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            var random = new Random();
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                result.Append(chars[random.Next(chars.Length)]);
            return result.ToString();
        }
    }
}