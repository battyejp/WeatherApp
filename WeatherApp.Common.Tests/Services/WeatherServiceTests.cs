using System;
using WeatherApp.Common.Services;
using Xunit;
using Flurl.Http.Testing;
using System.Linq;

namespace WeatherApp.Common.Tests.Services
{
    public class WeatherServiceTests
    {
        private WeatherService weatherService;

        public WeatherServiceTests()
        {
            weatherService = new WeatherService();
        }

        [Fact]
        public async void GetLocationsAsync_ReturnsNullWhenRepondsWithNot200()
        {
            using (var httpTest = new HttpTest())
            {
                // Arrange
                httpTest.RespondWith("some error", 500);

                // Act
                var result = await weatherService.GetLocationsAsync(10, 10);

                // Asert
                Assert.Null(result);
            }
        }

        [Fact]
        public async void GetLocationsAsync_ReturnsWithCorrectResults()
        {
            using (var httpTest = new HttpTest())
            {
                // Arrange
                httpTest.RespondWith("[{\"distance\":1836,\"title\":\"Santa Cruz\",\"location_type\":\"City\",\"woeid\":2488853,\"latt_long\":\"36.974018, -122.030952\"}]");

                // Act
                var results = await weatherService.GetLocationsAsync(10, 10);

                // Asert
                Assert.NotNull(results);
                Assert.True(results.Count == 1);
                var result = results.First();
                Assert.Equal("Santa Cruz", result.Title);
                Assert.Equal(2488853, result.WoeId);
                Assert.Equal("36.974018, -122.030952", result.Latt_Long);
            }
        }

        [Fact]
        public async void GetWeatherForecastAsync_ReturnsNullWhenRepondsWithNot200()
        {
            using (var httpTest = new HttpTest())
            {
                // Arrange
                httpTest.RespondWith("some error", 500);

                // Act
                var result = await weatherService.GetWeatherForecastAsync(10);

                // Asert
                Assert.Null(result);
            }
        }

        [Fact]
        public async void GetWeatherForecastAsync_ReturnsWithCorrectResults()
        {
            using (var httpTest = new HttpTest())
            {
                // Arrange
                httpTest.RespondWith("{\"consolidated_weather\":[{\"id\":4508455410860032,\"weather_state_name\":\"Heavy Cloud\",\"weather_state_abbr\":\"hc\",\"wind_direction_compass\":\"ENE\",\"created\":\"2018-06-07T17:53:02.027060Z\",\"applicable_date\":\"2018-06-07\",\"min_temp\":13.0875,\"max_temp\":21.66,\"the_temp\":19.59,\"wind_speed\":5.6499044110216339,\"wind_direction\":57.114590062595553,\"air_pressure\":1019.665,\"humidity\":69,\"visibility\":8.510299848882525,\"predictability\":71},{\"id\":5924058914881536,\"weather_state_name\":\"Showers\",\"weather_state_abbr\":\"s\",\"wind_direction_compass\":\"NNE\",\"created\":\"2018-06-07T17:53:02.223640Z\",\"applicable_date\":\"2018-06-08\",\"min_temp\":12.537500000000001,\"max_temp\":22.177500000000002,\"the_temp\":20.564999999999998,\"wind_speed\":5.2845336213035869,\"wind_direction\":29.852731171709603,\"air_pressure\":1019.75,\"humidity\":68,\"visibility\":9.9360360494710882,\"predictability\":73},{\"id\":6528150527803392,\"weather_state_name\":\"Showers\",\"weather_state_abbr\":\"s\",\"wind_direction_compass\":\"NE\",\"created\":\"2018-06-07T17:53:02.524420Z\",\"applicable_date\":\"2018-06-09\",\"min_temp\":12.635,\"max_temp\":22.255000000000003,\"the_temp\":20.719999999999999,\"wind_speed\":5.3397686481289268,\"wind_direction\":45.832999858699104,\"air_pressure\":1017.55,\"humidity\":69,\"visibility\":11.085572755110157,\"predictability\":73},{\"id\":5051081209937920,\"weather_state_name\":\"Showers\",\"weather_state_abbr\":\"s\",\"wind_direction_compass\":\"NNE\",\"created\":\"2018-06-07T17:53:02.814420Z\",\"applicable_date\":\"2018-06-10\",\"min_temp\":12.8825,\"max_temp\":21.432500000000001,\"the_temp\":20.629999999999999,\"wind_speed\":5.8748792654119946,\"wind_direction\":29.368789950069424,\"air_pressure\":1014.52,\"humidity\":68,\"visibility\":13.979609082955539,\"predictability\":73},{\"id\":4902218012557312,\"weather_state_name\":\"Showers\",\"weather_state_abbr\":\"s\",\"wind_direction_compass\":\"NNE\",\"created\":\"2018-06-07T17:53:02.833810Z\",\"applicable_date\":\"2018-06-11\",\"min_temp\":12.645,\"max_temp\":19.6525,\"the_temp\":21.009999999999998,\"wind_speed\":6.4308133660124867,\"wind_direction\":33.243060195952928,\"air_pressure\":1017.8399999999999,\"humidity\":65,\"visibility\":13.812770917839815,\"predictability\":73},{\"id\":6216798517067776,\"weather_state_name\":\"Showers\",\"weather_state_abbr\":\"s\",\"wind_direction_compass\":\"N\",\"created\":\"2018-06-07T17:53:04.928600Z\",\"applicable_date\":\"2018-06-12\",\"min_temp\":12.3325,\"max_temp\":19.27,\"the_temp\":18.719999999999999,\"wind_speed\":4.736617222089663,\"wind_direction\":5.6699661703321471,\"air_pressure\":1025.6600000000001,\"humidity\":66,\"visibility\":9.9978624830987037,\"predictability\":73}],\"time\":\"2018-06-07T19:34:14.752540+01:00\",\"sun_rise\":\"2018-06-07T04:45:12.298240+01:00\",\"sun_set\":\"2018-06-07T21:14:01.806237+01:00\",\"timezone_name\":\"LMT\",\"parent\":{\"title\":\"England\",\"location_type\":\"Region / State / Province\",\"woeid\":24554868,\"latt_long\":\"52.883560,-1.974060\"},\"sources\":[{\"title\":\"BBC\",\"slug\":\"bbc\",\"url\":\"http://www.bbc.co.uk/weather/\",\"crawl_rate\":180},{\"title\":\"Forecast.io\",\"slug\":\"forecast-io\",\"url\":\"http://forecast.io/\",\"crawl_rate\":480},{\"title\":\"HAMweather\",\"slug\":\"hamweather\",\"url\":\"http://www.hamweather.com/\",\"crawl_rate\":360},{\"title\":\"Met Office\",\"slug\":\"met-office\",\"url\":\"http://www.metoffice.gov.uk/\",\"crawl_rate\":180},{\"title\":\"OpenWeatherMap\",\"slug\":\"openweathermap\",\"url\":\"http://openweathermap.org/\",\"crawl_rate\":360},{\"title\":\"Weather Underground\",\"slug\":\"wunderground\",\"url\":\"https://www.wunderground.com/?apiref=fc30dc3cd224e19b\",\"crawl_rate\":720},{\"title\":\"World Weather Online\",\"slug\":\"world-weather-online\",\"url\":\"http://www.worldweatheronline.com/\",\"crawl_rate\":360},{\"title\":\"Yahoo\",\"slug\":\"yahoo\",\"url\":\"http://weather.yahoo.com/\",\"crawl_rate\":180}],\"title\":\"London\",\"location_type\":\"City\",\"woeid\":44418,\"latt_long\":\"51.506321,-0.12714\",\"timezone\":\"Europe/London\"}");

                // Act
                var result = await weatherService.GetWeatherForecastAsync(10);

                // Asert
                Assert.NotNull(result);
                Assert.True(result.Consolidated_Weather.Count == 6);
                var weather = result.Consolidated_Weather.First();
                Assert.Equal("Heavy Cloud", weather.Weather_State_Name);
                Assert.Equal(13.0875, weather.Min_Temp);
                Assert.Equal(21.66, weather.Max_Temp);
                Assert.Equal("2018-06-07", weather.Applicable_Date);
            }
        }
    }
}
