using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Common.Dtos
{
    public class ConsolidatedWeatherDto
    {
        public object Id { get; set; }
        public string Weather_State_Name { get; set; }
        public string Weather_State_Abbr { get; set; }
        public string Wind_Direction_Compass { get; set; }
        public DateTime Created { get; set; }
        public string Applicable_Date { get; set; }
        public double Min_Temp { get; set; }
        public double Max_Temp { get; set; }
        public double The_Temp { get; set; }
        public double Wind_Speed { get; set; }
        public double Wind_Direction { get; set; }
        public double Air_Pressure { get; set; }
        public int Humidity { get; set; }
        public double Visibility { get; set; }
        public int Predictability { get; set; }
    }
}
