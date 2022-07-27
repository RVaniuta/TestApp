using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestApp.Core.Clients.AppSettings;
using TestApp.Core.Clients.Interfaces;
using TestApp.Core.Clients.Mappings;
using TestApp.Core.Clients.Models;
using TestApp.Core.Models;

namespace TestApp.Core.Clients
{
    internal class OpenWeatherClient : IWeatherClient
    {
        private readonly HttpClient _client;

        private readonly OpenWeatherClientOptions _options;

        public ClientType Type => ClientType.OpenWeather;

        public OpenWeatherClient(HttpClient client, IOptions<OpenWeatherClientOptions> options)
        {
            _options = options.Value;
            _client = client;
            _client.BaseAddress = new Uri(_options.Hostname);
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<WeatherModel> Get(string city)
        {
            var path = string.Format(_options.Endpoint, city);
            var response = await _client.GetAsync($"{path}&appid={_options.ApiKey}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<OpenWeatherModel>
            (
                responseStream,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true}
            );

            return result.ToCommon();
        }
    }
}
