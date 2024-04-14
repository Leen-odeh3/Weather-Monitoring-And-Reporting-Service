
using System.Text.Json;
using static Weather_Monitoring_And_Reporting_Service.Weather;

namespace Weather_Monitoring_And_Reporting_Service.Strategies;

public class JsonFormatStrategy : ITextFormatStrategy
{
    public Weather GetWeatherData(string text)
    {
        Weather weatherData = JsonSerializer.Deserialize<Weather>(text);
        return weatherData;
    }
}
