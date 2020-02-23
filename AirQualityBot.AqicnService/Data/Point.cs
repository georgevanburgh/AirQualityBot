using Newtonsoft.Json;

namespace AirQualityBot.AqicnService.Data
{
    public class Point
    {
        [JsonConstructor]
        public Point(double[] points)
        {
            Lat = points[0];
            Long = points[1];
        }

        public double Lat { get; internal set; }
        public double Long { get; internal set; }
    }
}