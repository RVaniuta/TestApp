using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Core.Clients;
using TestApp.Core.Clients.Models;
using TestApp.Core.Models;

namespace TestApp.Core.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetData(string city, ClientType clientType);
    }
}