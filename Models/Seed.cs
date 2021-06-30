using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NapredniObrazec.DataBase;

namespace NapredniObrazec.Models
{
    public static class Seed
    {
        public static async Task CreateAdmin(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string[] roleNames = { "Admin", " " };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //var UserName = "timotej.arnus@gmail.com";
            var powerUser = new User
            {
                UserName = "timotej.arnus@gmail.com",
                Name = "Timotej",
                LastName = "Arnus",
                Email = "timotej.arnus@gmail.com",
                NormalizedEmail = "timotej.arnus@gmail.com".Normalize().ToUpper(),
            };

            var pass = "12345678aB!";

            var _user = await UserManager.FindByNameAsync("timotej.arnus@gmail.com");

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(powerUser, pass);

                if (createPowerUser.Succeeded)
                {
                    try
                    {
                        var result = await UserManager.AddToRoleAsync(powerUser, "Admin");  // Dodamo mu obe vlogi !
                        result = await UserManager.AddToRoleAsync(powerUser, "User");
                       
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }

        }
    }
}
