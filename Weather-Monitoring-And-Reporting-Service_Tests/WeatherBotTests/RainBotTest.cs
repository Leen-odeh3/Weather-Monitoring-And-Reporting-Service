using AutoFixture;
using FluentAssertions;
using Weather_Monitoring_And_Reporting_Service;
using Weather_Monitoring_And_Reporting_Service.WeatherBot;
namespace Weather_Monitoring_And_Reporting_Service_Tests.WeatherBotTests;

public class RainBotTest
{
    private readonly Fixture _fixture;

    public RainBotTest()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ProcessWeatherUpdate_Disabled_MustNotBeActivated()
    {
        var weatherData = _fixture.Build<Weather>().Create();
        var sut = new RainBot
        {
            Enabled = false,
            HumidityThreshold = _fixture.Create<int>(),
            Message = _fixture.Create<string>()
        };

        sut.ProcessWeatherUpdate(weatherData);
        sut.Activated.Should().BeFalse();
    }

    [Fact]
    public void ProcessWeatherUpdate_EnabledWithHumidityLowerThanHumidityThreshold_MustNotBeActivated()
    {

        var weatherData = _fixture.Build<Weather>().Create();
        var sut = new RainBot
        {
            Enabled = true,
            HumidityThreshold = _fixture.Create<int>(),
            Message = _fixture.Create<string>()
        };

        sut.ProcessWeatherUpdate(weatherData);

        sut.Activated.Should().BeFalse();
    }

    [Fact]
    public void ProcessWeatherUpdate_EnabledWithHumidityHigherThanHumidityThreshold_MustBeActivated()
    {
        var humidityThreshold = _fixture.Create<int>() + 10; 

        var weatherData = _fixture.Build<Weather>()
                                  .With(w => w.Humidity, humidityThreshold + 10) 
                                  .Create();
        var sut = new RainBot
        {
            Enabled = true,
            HumidityThreshold = humidityThreshold,
            Message = _fixture.Create<string>()
        };
        sut.ProcessWeatherUpdate(weatherData);
        sut.Activated.Should().BeTrue();
    }





}
