

using Weather_Monitoring_And_Reporting_Service;
using Weather_Monitoring_And_Reporting_Service.WeatherBot;

namespace Weather_Monitoring_And_Reporting_Service_Tests.WeatherBotTests;

public class RainBotTest
{

    [Fact]
    public void ProcessWeatherUpdate_Disabled_MustNotBeActivated()
    {
        var weather= new Weather { Humidity = 60 };
        var sut = new RainBot
        {
            Enabled = false,
            HumidityThreshold = 70,
            Message = "It's raining!"
        };
        sut.ProcessWeatherUpdate(weather);
        Assert.False(sut.Activated);
    }
    [Fact]

    public void ProcessWeatherUpdate_EnabledWithHumidityLowerThanHumidityThreshold_MustNotBeActivated()
    {
        var weatherData = new Weather { Humidity = 60 };
        var sut = new RainBot
        {
            Enabled = true,
            HumidityThreshold = 100,
            Message = "It's raining!"
        };
        sut.ProcessWeatherUpdate(weatherData);
        Assert.False(sut.Activated);
    }
    [Fact]

    public void ProcessWeatherUpdate_EnabledWithHumidityHigherThanHumidityThreshold_MustBeActivated()
    {
        var weatherData = new Weather { Humidity = 60 };
        var sut = new RainBot
        {
            Enabled = true,
            HumidityThreshold = 40,
            Message = "It's raining!"
        };
        sut.ProcessWeatherUpdate(weatherData);
        Assert.True(sut.Activated);
    }
}
