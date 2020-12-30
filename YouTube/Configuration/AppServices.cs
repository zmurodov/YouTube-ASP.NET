using YouTube.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using YouTube.Data;
using YouTube.BussinessManagers.Interfaces;
using YouTube.Service.Interfaces;
using YouTube.Service;
using YouTube.BussinessManagers;
using Microsoft.AspNetCore.Authorization;
using YouTube.Authorization;

namespace YouTube.Configuration
{
    public static class AppServices

    {
        public static void AddDefaultServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            serviceCollection.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            serviceCollection.AddControllersWithViews().AddRazorRuntimeCompilation();
            serviceCollection.AddRazorPages();

            serviceCollection.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }

        public static void AddCustomServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IVideoBussinessManager, VideoBussinessManager>();
            serviceCollection.AddScoped<IAdminBussinessManager, AdminBussinessManager>();
            serviceCollection.AddScoped<IHomeBussinessManager, HomeBussinessManager>();
            serviceCollection.AddScoped<IVideoService, VideoService>();
            serviceCollection.AddScoped<IUserService, UserService>();

        }

        public static void AddCustomAuthorization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAuthorizationHandler, VideoAuthorizationHandler>();
        }
    }
}
