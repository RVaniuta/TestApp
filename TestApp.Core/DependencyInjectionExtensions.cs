using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApp.Core.Cache.AppSettings;
using TestApp.Core.Cache.Interfaces;
using TestApp.Core.Clients;
using TestApp.Core.Clients.AppSettings;
using TestApp.Core.Clients.Interfaces;
using TestApp.Core.Services;
using TestApp.Core.Services.Interfaces;
using TestApp.Core.Cache;

namespace TestApp.Core
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddTestAppCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OpenWeatherClientOptions>(configuration.GetSection(nameof(OpenWeatherClientOptions)));

            services.Configure<CacheOptions>(configuration.GetSection(nameof(CacheOptions)));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });

            services.AddHttpClient<IWeatherClient, OpenWeatherClient>();

            services.AddScoped<ICacheHelper, DistributedCacheHelper>();

            services.AddScoped<IWeatherService, WeatherService>();

            return services;
        }
    }
}