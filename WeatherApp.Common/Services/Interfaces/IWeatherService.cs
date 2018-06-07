using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Common.Dtos;

namespace WeatherApp.Common.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<List<LocationDto>> GetLocationsAsync(double latitude, double longitude);
        Task<WeatherForecastDto> GetWeatherForecastAsync(int locationId);
    }
}