using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace AirQualityBot.AqicnService.Serialization
{
    internal class AqicnContractResolver : DefaultContractResolver
    {
        private static readonly Dictionary<string, string> PropertyMappings = new Dictionary<string, string>
        {
            {"Status", "status"}
        };

        protected override string ResolvePropertyName(string propertyName)
        {
            return PropertyMappings.TryGetValue(propertyName, out var name) ? name :  base.ResolvePropertyName(propertyName);
        }
    }
}