using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using TableTennisApp.Repository;
using TableTennisApp.Services;

namespace TableTennisApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                });

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddTransient<IPlayersService, PlayersService>();
            builder.Services.AddTransient<IQueueManager, QueueManager>();
            builder.Services.AddTransient<IQueueItemService, QueueItemService>();
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddScoped<IApplicationContext>(provider => provider.GetRequiredService<ApplicationContext>());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}