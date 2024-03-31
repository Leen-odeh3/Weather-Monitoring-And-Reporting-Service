
using System.Text.Json;
using static Weather_Monitoring_And_Reporting_Service.Weather;

namespace Weather_Monitoring_And_Reporting_Service.Strategies;

public class JsonFormatStrategy : ITextFormatStrategy
{
    public WeatherData GetWeatherData(string text)
    {
        WeatherData weatherData = JsonSerializer.Deserialize<WeatherData>(text);
        return weatherData;
    }
}
