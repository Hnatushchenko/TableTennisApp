using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using TableTennisApp.Data.Constants;
using TableTennisApp.Models;
using TableTennisApp.Repository;
using TableTennisApp.Services;

namespace TableTennisApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                options.JsonSerializerOptions.WriteIndented = true;
            });

            //builder.Services.AddScoped<RoleManager<ApplicationRole>>();

            IdentityBuilder identityBuilder = builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789¿‡¡·¬‚√„•¥ƒ‰≈Â™∫∆Ê«Á»Ë≤≥Øø…È ÍÀÎÃÏÕÌŒÓœÔ–—Ò“Ú”Û‘Ù’ı÷ˆ◊˜ÿ¯Ÿ˘¸ﬁ˛ﬂˇ-._ ";

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;

            }).AddEntityFrameworkStores<ApplicationContext>()
              .AddUserManager<ApplicationUserManager>()
              .AddRoleManager<RoleManager<ApplicationRole>>();

            

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(365);
                    options.Cookie.MaxAge = TimeSpan.FromDays(365);
                });

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddTransient<IPlayersService, PlayersService>();
            builder.Services.AddTransient<IQueueManager, QueueManager>();
            builder.Services.AddTransient<IGameService, GameService>();
            builder.Services.AddTransient<IQueueItemService, QueueItemService>();
            builder.Services.AddTransient<IRatingManager, RatingManager>();
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IApplicationContext>(provider => provider.GetRequiredService<ApplicationContext>());

            var app = builder.Build();

            await CreateRolesAsync(app.Services);

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<ApplicationUserManager>();
                string[] roleNames = { UserRoles.Admin, UserRoles.User };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExists = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        roleResult = await roleManager.CreateAsync(new ApplicationRole(roleName));
                    }
                }

                var admin = new ApplicationUser
                {
                    UserName = "¿Ì‰≥È √Ì‡ÚÛ˘ÂÌÍÓ",
                    Email = "gnatushenko.andrij@gmail.com",
                };

                string adminPassword = "123123Aa";
                var _user = await userManager.FindByEmailAsync(admin.Email);

                if (_user != null)
                {
                    await userManager.DeleteAsync(_user);
                }
                var createPowerUser = await userManager.CreateAsync(admin, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, UserRoles.Admin);
                }
            }
        }
    }
}