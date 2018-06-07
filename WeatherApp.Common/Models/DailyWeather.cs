using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Common.Dtos;

namespace WeatherApp.Common.Models
{
    public class DailyWeather : BaseModel
    {
        public string Weather { get; set; }
        public string WeatherAbbr { get; set; }
        public DateTime Date { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public int WoeId { get; set; }

        public static implicit operator DailyWeather(ConsolidatedWeatherDto dto) => new DailyWeather
        {
            Weather = dto.Weather_State_Name,
            Date = DateTime.Parse(dto.Applicable_Date),
            MaxTemp = dto.Max_Temp,
            MinTemp = dto.Min_Temp,
            WeatherAbbr = dto.Weather_State_Abbr
        };
    }
}
