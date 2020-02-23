using Newtonsoft.Json;

namespace AirQualityBot.AqicnService.Data
{
    internal class AirQualityResponse
    {
        [JsonProperty("status")]
        public string Status { get; internal set; }

        [JsonProperty("data")]
        public AirQualityData Data { get; internal set; }

    }
}