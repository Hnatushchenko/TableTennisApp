using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using TableTennisApp.Data.Extensions;
using TableTennisApp.Data.Repository;
using TableTennisApp.Models;
using TableTennisApp.Services;

namespace TableTennisApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                });

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
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

            
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddTransient<IQueueManager, QueueManager>();
            builder.Services.AddTransient<IGameService, GameService>();
            builder.Services.AddTransient<IQueueItemService, QueueItemService>();
            builder.Services.AddTransient<IRatingManager, RatingManager>();
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IApplicationContext>(provider => provider.GetRequiredService<ApplicationContext>());
            

            var app = builder.Build();

            var supportedCultures = new[]
            {
                new CultureInfo("uk"),
                new CultureInfo("da"),
            };

            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                //DefaultRequestCulture = new RequestCulture("uk"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(requestLocalizationOptions);

            await app.CreateInitialRolesAsync();
            await app.CreateDefaultAdminAsync();

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
    }
}