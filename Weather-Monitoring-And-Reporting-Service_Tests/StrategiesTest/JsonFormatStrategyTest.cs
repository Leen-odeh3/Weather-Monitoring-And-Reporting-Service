
using Weather_Monitoring_And_Reporting_Service.Strategies;
using Weather_Monitoring_And_Reporting_Service;
using System.Text.Json;

namespace Weather_Monitoring_And_Reporting_Service_Tests.StrategiesTest;

 public class JsonFormatStrategyTest
{

    [Fact]
    public void GetWeatherData_ValidJson_ReturnsWeatherObject()
    {
        // Arrange
        var json = "{\"Temperature\":25,\"Humidity\":50}";
        JsonFormatStrategy strategy = new JsonFormatStrategy();

        // Act
        Weather weather = strategy.GetWeatherData(json);

        // Assert
        Assert.NotNull(weather);
        Assert.Equal(25, weather.Temperature);
        Assert.Equal(50, weather.Humidity);
    }

    [Fact]
    public void GetWeatherData_InvalidJson_ThrowsException()
    {
        // Arrange
        string json = "Invalid JSON";
        JsonFormatStrategy strategy = new JsonFormatStrategy();

        // Act & Assert
        Assert.Throws<JsonException>(() => strategy.GetWeatherData(json));
    }
}


