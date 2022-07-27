using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestApp.Core.Clients.Helpers;
using TestApp.Core.Clients.Models;
using TestApp.Core.Models;

namespace TestApp.Core.Clients.Mappings
{
    public static class OpenWeatherMappings
    {
        public static WeatherModel ToCommon(this OpenWeatherModel model)
        {
            return new WeatherModel
            {
                City = model.City,
                Temperature = model.Main.Temp,
                WeatherCondition = new WeatherCondition
                {
                    Type = model.Weather.FirstOrDefault()?.Main,
                    Humidity = model.Main.Humidity,
                    Pressure = model.Main.Pressure
                },
                Wind = new Core.Models.Wind
                {
                    Speed = model.Wind.Speed,
                    Direction = model.Wind.Deg.ConvertToCompass()
                }

            };
        }
    }
}
