using System;
using System.IO;
using System.Threading.Tasks;
using AirQualityBot.AqicnService;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AirQualityBot.SkillEndpoint
{
    public static class AirQualityAlexa
    {
        private static IAirQualityService airQualityService = new AirQualityRestService(Environment.GetEnvironmentVariable("AQICN_API_TOKEN"));

        [FunctionName("AirQualityAlexa")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            SkillRequest skillRequest;
            using (var sr = new StreamReader(req.Body))
                skillRequest = JsonConvert.DeserializeObject<SkillRequest>(await sr.ReadToEndAsync());

            var response = await Handle(skillRequest);

            return response != null ? (IActionResult)new OkObjectResult(response) : new BadRequestObjectResult("Could not process request");
        }

        private static async Task<SkillResponse> Handle(SkillRequest request)
        {
            if (!(request.Request is IntentRequest intentRequest))
                return ResponseBuilder.Tell("I'm sorry - I didn't understand your request");

            switch (intentRequest.Intent.Name)
            {
                case "CurrentAirQualityForCity":
                    return await GetAirQualityForCity(intentRequest);
                case "CurrentAirQualityForDeviceLocation":
                    return await GetAirQualityForDeviceLocation(intentRequest, request.Context.Geolocation);
                default:
                    return ResponseBuilder.Tell("Error - unknown intent");
            }

        }

        private static async Task<SkillResponse> GetAirQualityForCity(IntentRequest request)
        {
            if (!request.Intent.Slots.TryGetValue("location", out var location))
                return ResponseBuilder.Tell("I didn't hear you say a city");

            var aqin = await airQualityService.GetAirQualityForCityAsync(location.Value);

            return ResponseBuilder.Tell($"Air quality index in {location.Value} is currently {aqin.AirQualityIndex}");
        }

        private static async Task<SkillResponse> GetAirQualityForDeviceLocation(IntentRequest request, Geolocation location)
        {
            if (location == null || location.LocationServices.Access == LocationServiceAccess.Disabled)
                return ResponseBuilder.Tell("Error - device location could not be retrieved");

            var aqin = await airQualityService.GetAirQualityForLocation(location.Coordinate.Latitude,
                location.Coordinate.Longitude);

            return ResponseBuilder.Tell($"Air quality index in {aqin.City.Name} is currently {aqin.AirQualityIndex}");
        }
    }
}
