using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Common.Dtos
{
    public class ParentDto
    {
        public string Title { get; set; }
        public string Location_Type { get; set; }
        public int WoeId { get; set; }
        public string Latt_Long { get; set; }
    }
}
