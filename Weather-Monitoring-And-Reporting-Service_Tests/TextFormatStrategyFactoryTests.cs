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
        // Arrange & Act
        Action act = () => TextFormatStrategyFactory.GetTextFormatStrategy(invalidWeatherDataFilePath);

        // Assert
        act.Should().Throw<WeatherDataFilePathNullException>()
            .WithMessage("Weather data file path is null or empty.");
    }
/*
    [Fact]
    public void GetTextFormatStrategy_UnsupportedFileExtension_ReturnsNull()
    {
        string weatherDataFilePath = "x.doc";

        TextFormatStrategyFactory.GetTextFormatStrategy(weatherDataFilePath)
                             .Should().BeNull();
    }

    */

    [Fact]
    public void GetTextFormatStrategy_JsonFileExtension_ReturnsJsonStrategy()
    {
        string weatherDataFilePath = "x.json";

        TextFormatStrategyFactory.GetTextFormatStrategy(weatherDataFilePath)
                            .Should().BeOfType<JsonFormatStrategy>();
    }

    [Fact]
    public void GetTextFormatStrategy_XmlFileExtension_ReturnsXmlStrategy()
    {
        string weatherDataFilePath = "x.xml";

        TextFormatStrategyFactory.GetTextFormatStrategy(weatherDataFilePath)
                            .Should().BeOfType<XmlFormatStrategy>();
    }
}
