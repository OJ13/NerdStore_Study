using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.Identidade.API.Data;
using NSE.Identidade.API.Extensions;
using NSE.WebApi.Core.Identidade;
using NSE.WebApi.Core.Model;

namespace NSE.Identidade.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddAIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            #region EntityFramework Config
            services.AddDbContext<ApplicationDbContext>(optionsAction: options =>
                options.UseSqlServer(configuration.GetConnectionString(name: "DefaultConnection")));
            #endregion

            #region Identity Config
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMensagensPtBr>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region JWT Config
            services.AddJwtConfiguration(configuration);
            #endregion

            return services;
        }
    }
}
