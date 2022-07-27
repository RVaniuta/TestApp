using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Core.Cache;
using TestApp.Core.Cache.AppSettings;
using TestApp.Core.Cache.Interfaces;
using TestApp.Core.Clients;
using TestApp.Core.Clients.Interfaces;
using TestApp.Core.Clients.Models;
using TestApp.Core.Models;
using TestApp.Core.Services.Interfaces;

namespace TestApp.Core.Services
{
    internal class WeatherService : IWeatherService
    {
        private readonly IEnumerable<IWeatherClient> _clients;
        private readonly ICacheHelper _cacheHelper;
        private readonly CacheOptions _cacheOptions;

        public WeatherService(IEnumerable<IWeatherClient> clients, ICacheHelper cacheHelper, IOptions<CacheOptions> cacheOptions)
        {
            _clients = clients;
            _cacheHelper = cacheHelper;
            _cacheOptions = cacheOptions.Value;
        }

        public async Task<WeatherModel> GetData(string city, ClientType clientType)
        {
            var client = _clients.FirstOrDefault(x => x.Type == clientType);

            return await _cacheHelper.GetFromCacheAsync(() =>
                client.Get(city),
                key: $"{clientType}_{city}_{CacheKeys.WeatherInfoKey}",
                _cacheOptions.ExpirationSeconds
            );
        }
    }
}
