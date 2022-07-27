using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Core.Clients.Models;
using TestApp.Core.Models;

namespace TestApp.Core.Clients.Interfaces
{
    interface IWeatherClient
    {
        ClientType Type { get; }

        Task<WeatherModel> Get(string city);
    }
}
