﻿using BagShop.Data;
using BagShop.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BagShop.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<BagShopDbContext>().Database.Migrate();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                Task
                    .Run(async () =>
                    {
                        var adminName = GlobalConstants.AdministratorRole;
                        var roleExists = await roleManager.RoleExistsAsync(adminName);

                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = adminName
                            });
                        }
                        var adminEmail = "admin@mysite.com";

                        var adminUser = await userManager.FindByNameAsync(adminName);
                        if (adminUser == null)
                        {
                            adminUser =  new User
                                {
                                    Email = adminEmail,
                                    UserName = adminEmail
                                };
                            await userManager.CreateAsync(adminUser, "admin12");
                            await userManager.AddToRoleAsync(adminUser, adminName);
                        };

                    })
                    .Wait();
            }

            return app;
        }
    }
}
