using AirQualityBot.AqicnService.Data;

namespace AirQualityBot.AqicnService;

public interface IAirQualityService
{
    Task<AirQualityData> GetAirQualityForCityAsync(string cityName);
    Task<AirQualityData> GetAirQualityForLocation(double latitude, double longitude);
}
