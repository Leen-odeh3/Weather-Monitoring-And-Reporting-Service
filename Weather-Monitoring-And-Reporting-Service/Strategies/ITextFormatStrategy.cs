﻿
using static Weather_Monitoring_And_Reporting_Service.Weather;

namespace Weather_Monitoring_And_Reporting_Service.Strategies;

public interface ITextFormatStrategy
{
    public Weather GetWeatherData(string text);
}
