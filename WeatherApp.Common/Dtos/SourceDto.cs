using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Common.Dtos
{
    public class SourceDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public int Crawl_Rate { get; set; }
    }
}
