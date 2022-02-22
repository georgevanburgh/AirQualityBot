using Newtonsoft.Json;

namespace AirQualityBot.AqicnService.Data;

internal record AirQualityResponse
{
    [JsonProperty("status")]
    public string Status { get; init; }

    [JsonProperty("data")]
    public AirQualityData Data { get; init; }

}
