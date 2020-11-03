using GamesRental.Entities;
using GamesRental.Entities.Enuns;
using GamesRental.Infrastructure.Data.Context;
using GamesRental.Util;
using System.Linq;

namespace GamesRental.Infrastructure.Data.Data
{
    public static class DbInitializer
    {
        public static void Initializer(BaseContext context)
        {
            if (context.User.Any())
                return;
            else
            {
                var defaultUser = new User();
                defaultUser.Name = "Admin";
                defaultUser.Email = "admin@admin.com";
                defaultUser.Hash = HashMD5.getMD5("admin@admin.com");
                defaultUser.Password = HashMD5.getMD5("123456");
                defaultUser.Role = (int)RoleUser.Administrator;

                context.Add(defaultUser);
                context.SaveChanges();
            }
        }
    }
}
