using System;
using System.Linq;
using Newtonsoft.Json;
using WannaTravel.BusinessLogic.Entities;

namespace WannaTravel.CommentsSender
{
    public static class CommentGenerator
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();

        public static string GenerateRandomComment()
        {
            var comment = new CommentEntity
            {
                CommentatorName = RandomString(),
                Rate = RandomRate(),
                RestaurantId = 1,
                Text = RandomString()
            };
            return JsonConvert.SerializeObject(comment);
        }

        private static string RandomString()
        {
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static int RandomRate()
        {
            return random.Next(1, 5);
        }
    }
}
