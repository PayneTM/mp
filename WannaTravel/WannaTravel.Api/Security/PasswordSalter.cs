using System;
using System.Configuration;
using System.Linq;

namespace WannaTravel.Api.Security
{
    public static class PasswordSalter
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-=_+[]<>";

        public static string SaltPassword(string password, string salt)
        {
            var apiSalt = ConfigurationManager.AppSettings["ApiSalt"];
            var salted = $"{apiSalt}{password}{salt}";
            return salted;
        }

        public static string GetSalt(int length)
        {
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}