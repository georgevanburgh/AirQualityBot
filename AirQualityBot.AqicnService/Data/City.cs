using Newtonsoft.Json;

namespace AirQualityBot.AqicnService.Data;

public record City
{
    [JsonProperty("name")]
    public string Name { get; init; }

    [JsonProperty("url")]
    public Uri Url { get; init; }
}
