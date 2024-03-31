
using static Weather_Monitoring_And_Reporting_Service.Weather;
using System.Xml.Serialization;

namespace Weather_Monitoring_And_Reporting_Service.Strategies;

public class XmlFormatStrategy : ITextFormatStrategy
{
    public WeatherData GetWeatherData(string text)
    {

        XmlSerializer serializer = new XmlSerializer(typeof(WeatherData));

        using (TextReader reader = new StringReader(text))
        {
            return (WeatherData)serializer.Deserialize(reader);
        }

    }

}