using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Commons.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Commons.Identity.DummyData
{
    public class DummyAdmin
    {
        public static async Task Initialize<TUser>(UserService<TUser> user) 
            where TUser : CommonsUser, new()
        {
            if (!await user.Users.AnyAsync(x => x.UserName == "admin@admin.com"))
            {
                var admin = new TUser()
                {
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com",
                    EmailConfirmed = true,
                };

                var result = await user.CreateAsync(admin, "Iscoders123!");

                if (result.Succeeded) await user.AddClaimAsync(admin, new Claim("IsAdmin", DateTime.Now.ToShortDateString()));
            }
        }

        public static async Task SetAdmin<TUser>(UserService<TUser> user, string username)
            where TUser : CommonsUser, new()
        {
            var admin = await user.FindByNameAsync(username);
            if (admin != null)
            {
                var claims = await user.GetClaimsAsync(admin);
                await user.RemoveClaimsAsync(admin, claims.Where(x => x.Type == "IsAdmin"));
                await user.AddClaimAsync(admin, new Claim("IsAdmin", DateTime.Now.ToShortDateString()));
            }
        }
    }
}
