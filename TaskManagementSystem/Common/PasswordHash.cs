using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCrypt.Net;

namespace TaskManagementSystem.Common
{
    public static class PasswordHash
    {
        public static string CreateHashBCrypt(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10, 'b');
            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hash;
        }

        public static bool VerifyHashBCrypt(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}