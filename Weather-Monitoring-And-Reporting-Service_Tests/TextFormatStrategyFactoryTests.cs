using FluentAssertions;
using Weather_Monitoring_And_Reporting_Service.Strategies;
using Weather_Monitoring_And_Reporting_Service;

namespace Weather_Monitoring_And_Reporting_Service_Tests;

public class TextFormatStrategyFactoryTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetTextFormatStrategy_EmptyOrNullString_ThrowsException(string? invalidWeatherDataFilePath)
    {
        Action act = () => TextFormatStrategyFactory.GetTextFormatStrategy(invalidWeatherDataFilePath);

        act.Should().Throw<WeatherDataFilePathNullException>();

    }

    [Fact]
    public void GetTextFormatStrategy_JsonFileExtension_ReturnsJsonStrategy()
    {
        string weatherDataFilePath = "x.json";

        var strategy = TextFormatStrategyFactory.GetTextFormatStrategy(weatherDataFilePath);
        strategy.Should().BeOfType<JsonFormatStrategy>();
    }

    [Fact]
    public void GetTextFormatStrategy_XmlFileExtension_ReturnsXmlStrategy()
    {
        string weatherDataFilePath = "x.xml";

        var strategy = TextFormatStrategyFactory.GetTextFormatStrategy(weatherDataFilePath);
        strategy.Should().BeOfType<XmlFormatStrategy>();
    }
}
