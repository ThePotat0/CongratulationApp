using CongratulationApp.Domains;
using CongratulationApp.Domains.Repositories.Abstract;
using CongratulationApp.Domains.Repositories.EntityFranework;
using CongratulationApp.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CongratulationApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Enable appsettings.json
            IConfigurationBuilder configurationBuild = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Objecting Project section
            IConfiguration configuration = configurationBuild.Build();
            // Get<AppConfig>()! means that we shure that the Project section and appsettings already loaded, and the compiler can compile it safety
            AppConfig config = configuration.GetSection("Project").Get<AppConfig>()!;

            // Database context connection
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(config.Database.ConnectionString)
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
            );

            builder.Services.AddTransient<IContactsRepository, EFContactsRepository>();
            builder.Services.AddTransient<DataManager>();

            // Configure Identity system
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            // Configure Authentification cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "CongratulationAppAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/admin/accessdenied";
                options.SlidingExpiration = true;
            });


            // Enable Controllers (MVC)
            builder.Services.AddControllersWithViews();

            // Assemble the configuration
            WebApplication app = builder.Build();
            // Middleware pipeline

            // Enable using static files (js, css, etc.)
            app.UseStaticFiles();

            // Enable routing system
            app.UseRouting();

            // Enable authorization and authentication
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            // Routes register
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            await app.RunAsync();
        }
    }
}
