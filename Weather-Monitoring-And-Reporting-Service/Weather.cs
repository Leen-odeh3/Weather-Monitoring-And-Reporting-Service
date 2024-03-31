
using System.Xml.Serialization;

namespace Weather_Monitoring_And_Reporting_Service;

public class Weather 
{
    public static implicit operator Weather(WeatherData v)
    {
        throw new NotImplementedException();
    }

    [XmlRoot("Weather")]
    public class WeatherData
    {
        [XmlElement("Location")]
        public string Location { get; init; }

        [XmlElement("Humidity")]
        public double Humidity { get; init; }
        [XmlElement("Temperature")]
        public double Temperature { get; init; }
    }
}
