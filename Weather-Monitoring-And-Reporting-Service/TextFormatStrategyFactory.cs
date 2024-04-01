

using Weather_Monitoring_And_Reporting_Service.Strategies;

namespace Weather_Monitoring_And_Reporting_Service;

public static class TextFormatStrategyFactory
{

    public static ITextFormatStrategy? GetTextFormatStrategy(string? weatherDataFilePath)
    {
        if (String.IsNullOrEmpty(weatherDataFilePath))
        {
            throw new ArgumentNullException(nameof(weatherDataFilePath));
        }

        string fileExtension = GetFileExtension(weatherDataFilePath);

        if (fileExtension.Equals("json")) return new JsonFormatStrategy();
        if (fileExtension.Equals("xml")) return new XmlFormatStrategy();

        else return null;
    }

    private static string GetFileExtension(string? weatherDataFilePath)
    {
        if (String.IsNullOrEmpty(weatherDataFilePath))
        {
            throw new ArgumentNullException(nameof(weatherDataFilePath));
        }
        return weatherDataFilePath.Split('.').Last();
    }
}
