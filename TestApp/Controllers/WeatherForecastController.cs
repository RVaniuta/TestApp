using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core.Clients;
using TestApp.Core.Clients.Models;
using TestApp.Core.Models;
using TestApp.Core.Services.Interfaces;

namespace TestApp.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("v{version:apiVersion}")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet]
        [Route("getCurrentWeather")]
        public async Task<WeatherModel> Get(string city)
        {
            return await _weatherService.GetData(city, ClientType.OpenWeather);
        }
    }
}
