using System;
using Newtonsoft.Json;

namespace AirQualityBot.AqicnService.Data
{
    public class City
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("url")]
        public Uri Url { get; internal set; }
        //[JsonProperty("geo")]
        //public Point Geo { get; internal set; }

    }
}