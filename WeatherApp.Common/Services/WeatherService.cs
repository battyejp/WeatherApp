using Flurl.Http;
using Flurl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Common.Dtos;
using WeatherApp.Common.Services.Interfaces;

namespace WeatherApp.Common.Services
{
    public class WeatherService : IWeatherService
    {
        public async Task<List<LocationDto>> GetLocationsAsync(double latitude, double longitude)
        {
            List<LocationDto> results;
            try
            {
                results = await "https://www.metaweather.com/api/location/search/"
                    .SetQueryParam("lattlong", $"{latitude},{longitude}")
                    .GetJsonAsync<List<LocationDto>>();
            }
            catch (Exception ex)
            {
                //TO DO log to remote logging service
                return null;
            }

            return results;
        }

        public async Task<WeatherForecastDto> GetWeatherForecastAsync(int locationId)
        {
            WeatherForecastDto result;

            try
            {
                var url = $"https://www.metaweather.com/api/location/{locationId.ToString()}/";
                result = await url.GetJsonAsync<WeatherForecastDto>();
            }
            catch (Exception ex)
            {
                //TO DO log to remote logging service
                return null;
            }

            return result;
        }
    }
}