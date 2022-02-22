using Newtonsoft.Json;

namespace AirQualityBot.AqicnService.Data;

public record AirQualityData
{
    [JsonProperty("idx")]
    public int StationId { get; init; }

    [JsonProperty("aqi")]
    public int AirQualityIndex { get; init; }

    [JsonProperty("attributions")]
    public List<Attribution> Attributions { get; init; }

    [JsonProperty("dominentpol")]
    public string DominentPolutant { get; init; }

    [JsonProperty("city")]
    public City City { get; init; }
}

public record Attribution
{
    [JsonProperty("url")]
    public Uri Url { get; init; }

    [JsonProperty("name")]
    public Uri Name { get; init; }
}
