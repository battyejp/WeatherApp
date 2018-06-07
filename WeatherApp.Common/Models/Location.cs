using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Common.Dtos;

namespace WeatherApp.Common.Models
{
    public class Location : BaseModel
    {
        public string Title { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }

        public int WoeId { get; set; }

        public static implicit operator Location(LocationDto dto) => new Location
        {
            Id = dto.WoeId,
            Title = dto.Title,
            Lat = dto.Latt_Long.Split(',')[0],
            Long = dto.Latt_Long.Split(',')[1],
            WoeId = dto.WoeId
        };
    }
}

