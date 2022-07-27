using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Core.Clients.Helpers
{
    internal static class DegToCompassHelperExtensions
    {
        private static string[] compass = new[] { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };

        public static string ConvertToCompass(this double deg)
        {
            int val = Convert.ToInt32((deg / 22.5) + 0.5);
            return compass[(val % 16)];
        }
    }
}
