using Weather_Monitoring_And_Reporting_Service.Strategies;

namespace Weather_Monitoring_And_Reporting_Service
{
    public enum FileExtension
    {
        Json,
        Xml
    }

    public static class TextFormatStrategyFactory
    {
        public static ITextFormatStrategy? GetTextFormatStrategy(string? weatherDataFilePath)
        {
            if (String.IsNullOrEmpty(weatherDataFilePath))
            {
                throw new WeatherDataFilePathNullException("Weather data file path is null or empty.");
            }

            FileExtension fileExtension = GetFileExtension(weatherDataFilePath);

            switch (fileExtension)
            {
                case FileExtension.Json:
                    return new JsonFormatStrategy();
                case FileExtension.Xml:
                    return new XmlFormatStrategy();
                default:
                    return null;
            }
        }

        private static FileExtension GetFileExtension(string? weatherDataFilePath)
        {
            string extension = Path.GetExtension(weatherDataFilePath)?.TrimStart('.');
            return Enum.Parse<FileExtension>(extension, true);
        }
    }
}
