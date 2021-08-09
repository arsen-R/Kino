using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KinoSite.Data;
using KinoSite.Models;
using KinoSite.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace KinoSite
{
    public class DataInitializer
    {
        // movie, anime, tv-series, show-tv-series, anime-tv-series
        public static async Task Initialize(ApplicationContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> user)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { NameCategory = "movie" },
                    new Category { NameCategory = "anime" },
                    new Category { NameCategory = "tv-series" },
                    new Category { NameCategory = "show-tv-series" },
                    new Category { NameCategory = "anime-tv-series" }
                    );
            }

            context.SaveChanges();
            if (await roleManager.FindByNameAsync("Administrator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            if (await user.FindByEmailAsync("arsenr412@gmail.com") == null)
            {
                ApplicationUser admin = new ApplicationUser()
                {
                    Email = "arsenr412@gmail.com",
                    UserName = "arsenr412@gmail.com",
                    Name = "Administrator"
                };
                IdentityResult result = await user.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    await user.AddToRoleAsync(admin, "Administrator");
                }
            }
        }
    }
}
