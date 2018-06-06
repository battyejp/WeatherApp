using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Common.Dtos;

namespace WeatherApp.Common
{
    public class WeatherService
    {
        //TODO unit test this
        public async Task<List<LocationDto>> GetLocationsAsync()
        {
            List<LocationDto> results;
            try
            {
                results = await "https://www.metaweather.com/api/location/search/?lattlong=36.96,-122.02".GetJsonAsync<List<LocationDto>>(); //TODO move to config and pass in variables
            }
            catch (Exception ex)
            {
                //TODO log to remote logging service
                return null;
            }

            return results;
        }

        public async Task<WeatherForecastDto> GetWeatherForecastAsync()
        {
            WeatherForecastDto result;

            try
            {
                result = await "https://www.metaweather.com/api/location/44418/".GetJsonAsync<WeatherForecastDto>(); //TODO move to config and pass in variables
            }
            catch (Exception ex)
            {
                //TODO log to remote logging service
                return null;
            }

            return result;
        }
    }
}