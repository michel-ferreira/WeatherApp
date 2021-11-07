using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class CountryResponse
    {
        [JsonProperty("name")]
        public CountryCommon Name { get; set; }
        [JsonProperty("flags")]
        public Flag Image { get; set; }
    }
    public class CountryCommon
    {
        [JsonProperty("common")]
        public string CountryName { get; set; }
    }
    public class Flag
    {
        [JsonProperty("png")]
        public string Image { get; set; }
    }
}
