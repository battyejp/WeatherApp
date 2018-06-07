using Flurl.Http;
using Flurl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Common.Dtos;

namespace WeatherApp.Common
{
    public class WeatherService
    {
        //TODO unit test this
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