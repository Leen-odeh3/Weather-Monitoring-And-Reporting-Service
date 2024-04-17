
using Weather_Monitoring_And_Reporting_Service;
using Weather_Monitoring_And_Reporting_Service.WeatherBot;

namespace Weather_Monitoring_And_Reporting_Service_Tests.WeatherBotTests;
public class SunBotTest
{
    [Fact]
    public void ProcessWeatherUpdate_Disabled_MustNotBeActivated()
    {
        var weatherData = new Weather { Temperature = 35 };
        var sut = new SunBot
        {
            Enabled = false,
            TemperatureThreshold = 30,
            Message = "It's sunny!"
        };

        sut.ProcessWeatherUpdate(weatherData);

        Assert.False(sut.Activated);
    }

    [Fact]
    public void ProcessWeatherUpdate_EnabledWithTemperatureHigherThanTemperatureThreshold_MustBeActivated()
    {
        var weatherData = new Weather { Temperature = 35 };
        var sut = new SunBot
        {
            Enabled = true,
            TemperatureThreshold = 30,
            Message = "It's sunny!"
        };

        sut.ProcessWeatherUpdate(weatherData);

        Assert.True(sut.Activated);
    }

    [Fact]
    public void ProcessWeatherUpdate_EnabledWithTemperatureLowerThanTemperatureThreshold_MustNotBeActivated()
    {
        // Arrange
        var weatherData = new Weather { Temperature = 20 };
        var sut = new SunBot
        {
            Enabled = true,
            TemperatureThreshold = 30,
            Message = "It's sunny!"
        };

        // Act
        sut.ProcessWeatherUpdate(weatherData);

        // Assert
        Assert.False(sut.Activated, "The SunBot should not be activated when temperature is lower than the threshold.");
    }

}
