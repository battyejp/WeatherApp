using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Common.Dtos;

namespace WeatherApp.Common.Models
{
    public class DailyWeather : BaseModel
    {
        public string Weather { get; set; }
        public DateTime Date { get; set; }
        public double MinTemp { get; set; }
        public double Max_Temp { get; set; }
        public int WoeId { get; set; }

        public static implicit operator DailyWeather(ConsolidatedWeatherDto dto) => new DailyWeather
        {
            Weather = dto.Weather_State_Name,
            Date = DateTime.Parse(dto.Applicable_Date),
            Max_Temp = dto.Max_Temp,
            MinTemp = dto.Min_Temp,
        };
    }
}
