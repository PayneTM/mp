using System;
using WannaTravel.Api.Models;
using WannaTravel.Models.DbEntities;

namespace WannaTravel.Api.Helpers.Extensions
{
    public static class UserMappingExtention
    {
        public static ApplicationUser ToApplicationUser(this RegisterUserModel model)
        {
            return new ApplicationUser
            {
                Email = model.Email,
                EmailConfirmed = true,
                JoinDate = DateTime.UtcNow,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                PhoneNumberConfirmed = true,
                UserName = model.Username,
                TwoFactorEnabled = false
            };
        }
    }
}