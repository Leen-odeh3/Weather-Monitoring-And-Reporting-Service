
using Weather_Monitoring_And_Reporting_Service.WeatherBot;

namespace Weather_Monitoring_And_Reporting_Service.Configuration;

public class BotConfiguration
{
    public RainBot RainBot { get; init; }
    public SunBot SunBot { get; init; }
    public SnowBot SnowBot { get; init; }
}
