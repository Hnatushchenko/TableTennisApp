using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TableTennisApp.Data.Constants;
using TableTennisApp.Models;
using TableTennisApp.Services;

namespace TableTennisApp.Data.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public async static Task<IApplicationBuilder> CreateInitialRolesAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            foreach (var roleName in UserRoles.AllRoles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }
            
            return app;
        }

        public async static Task<IApplicationBuilder> CreateDefaultAdminAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<ApplicationUserManager>();

            var admin = new ApplicationUser
            {
                UserName = "Андрій Гнатущенко",
                Email = "gnatushenko.andrij@gmail.com",
            };

            string adminPassword = "123123Aa";
            var user = await userManager.FindByEmailAsync(admin.Email);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(admin, adminPassword);
                user = admin;
            }

            await userManager.AddToRoleAsync(user, UserRoles.Admin);
            await userManager.AddToRoleAsync(user, UserRoles.Referee);
            await userManager.AddToRoleAsync(user, UserRoles.User);

            return app;
        }
    }
}
