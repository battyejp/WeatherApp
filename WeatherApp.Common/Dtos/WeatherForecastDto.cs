using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Common.Dtos
{
    public class WeatherForecastDto
    {
        public List<ConsolidatedWeatherDto> Consolidated_Weather { get; set; }
        public DateTime Time { get; set; }
        public DateTime Sun_Rise { get; set; }
        public DateTime Sun_Set { get; set; }
        public string Timezone_Name { get; set; }
        public ParentDto Parent { get; set; }
        public List<SourceDto> Sources { get; set; }
        public string Title { get; set; }
        public string Location_Type { get; set; }
        public int WoeId { get; set; }
        public string Latt_Long { get; set; }
        public string Timezone { get; set; }
    }
}
