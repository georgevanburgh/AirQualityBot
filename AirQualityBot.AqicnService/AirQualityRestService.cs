using AirQualityBot.AqicnService.Data;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace AirQualityBot.AqicnService;

public class AirQualityRestService : IAirQualityService
{
    private RestClient restClient;

    public AirQualityRestService(string apiToken)
    {
        restClient = new RestClient("https://api.waqi.info");
        restClient.AddDefaultQueryParameter("token", apiToken);

        restClient.UseNewtonsoftJson();
    }

    public async Task<AirQualityData> GetAirQualityForCityAsync(string cityName)
    {
        var request = new RestRequest("/feed/{city}/")
            .AddParameter("city", cityName, ParameterType.UrlSegment);

        return await Execute(request);
    }

    public async Task<AirQualityData> GetAirQualityForLocation(double latitude, double longitude)
    {
        var request = new RestRequest("/feed/geo:{lat};{lng}/")
            .AddParameter("lat", latitude, ParameterType.UrlSegment)
            .AddParameter("lng", longitude, ParameterType.UrlSegment);

        return await Execute(request);
    }

    private async Task<AirQualityData> Execute(RestRequest request)
    {
        var response = await restClient.ExecuteAsync<AirQualityResponse>(request);

        if (!response.IsSuccessful || response.Data.Status != "ok")
            return null; // TODO: Handle error

        return response.Data.Data;
    }
}
