
using System.Xml.Serialization;
using Weather_Monitoring_And_Reporting_Service;
using Weather_Monitoring_And_Reporting_Service.Strategies;

namespace Weather_Monitoring_And_Reporting_Service_Tests.StrategiesTest;

public class XmlFormatStrategyTest
{ 

    [Fact]
    public void DeserializeWeather_ValidXml_ReturnsWeatherObject()
    {
        // Arrange
        var xml = @"<Weather><Temperature>25</Temperature><Humidity>50</Humidity></Weather>";
        var strategy = new XmlFormatStrategy();

        // Act
        var weather = strategy.DeserializeWeather(xml);

        // Assert
        Assert.NotNull(weather);
        Assert.Equal(25, weather.Temperature);
        Assert.Equal(50, weather.Humidity);
        // Add more assertions based on your Weather class properties
    }

    [Fact]
    public void DeserializeWeather_InvalidXml_ThrowsException()
    {
        var xml = "Invalid XML";
        var strategy = new XmlFormatStrategy();

        Assert.Throws<InvalidOperationException>(() => strategy.DeserializeWeather(xml));
    }
}

