using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AirQualityBot.AqicnService.Data
{
    public class AirQualityData
    {
        [JsonProperty("idx")]
        public int StationId { get; internal set; }

        [JsonProperty("aqi")]
        public int AirQualityIndex { get; internal set; }

        [JsonProperty("attributions")]
        public List<Attribution> Attributions { get; internal set; }

        [JsonProperty("dominentpol")]
        public string DominentPolutant { get; internal set; }

        [JsonProperty("city")]
        public City City { get; internal set; }
    }

    public class Attribution
    {
        [JsonProperty("url")]
        public Uri Url { get; internal set; }

        [JsonProperty("name")]
        public Uri Name { get; internal set; }
    }
}