

using Weather_Monitoring_And_Reporting_Service;
using Weather_Monitoring_And_Reporting_Service.WeatherBot;

namespace Weather_Monitoring_And_Reporting_Service_Tests.WeatherBotTests;

public class SnowBotTest
{
    [Fact]
    public void ProcessWeatherUpdate_Disabled_MustNotBeActivated()
    {
        var weatherData = new Weather { Temperature = -5 };
        var sut = new SnowBot
        {
            Enabled = false,
            TemperatureThreshold = 0,
            Message = "It's snowing!"
        };

        sut.ProcessWeatherUpdate(weatherData);

        Assert.False(sut.Activated);
    }

    [Fact]
    public void ProcessWeatherUpdate_EnabledWithTemperatureLowerThanTemperatureThreshold_MustBeBeActivated()
    {
        var weatherData = new Weather { Temperature = 0 };
        var sut = new SnowBot
        {
            Enabled = true,
            TemperatureThreshold = 10,
            Message = "It's snowing!"
        };

        sut.ProcessWeatherUpdate(weatherData);

        Assert.True(sut.Activated);
    }

    [Fact]
    public void ProcessWeatherUpdate_EnabledWithTemperatureHigherThanTemperatureThreshold_MustNotBeBeActivated()
    {
        var weatherData = new Weather { Temperature = 20 };
        var sut = new SnowBot
        {
            Enabled = true,
            TemperatureThreshold = 0,
            Message = "It's snowing!"
        };

        sut.ProcessWeatherUpdate(weatherData);

        Assert.False(sut.Activated);
    }
}
