using System;
namespace WeatherApp.Common.Dtos
{
    public class LocationDto
    {
        public int Distance { get; set; }
        public string Title { get; set; }
        public string Location_Type { get; set; }
        public int WoeId { get; set; }
        public string Latt_Long { get; set; }
    }
}
