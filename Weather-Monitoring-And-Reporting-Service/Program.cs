using System.Text.Json;
using Weather_Monitoring_And_Reporting_Service.Configuration;
using Weather_Monitoring_And_Reporting_Service.Publisher;
using Weather_Monitoring_And_Reporting_Service.Strategies;
using static Weather_Monitoring_And_Reporting_Service.Weather;

namespace Weather_Monitoring_And_Reporting_Service;


internal class Program
{
    static void Main(string[] args)
    {
        #region Load Bot Configurations
        BotConfiguration? botConfig = LoadConfig();

        if (botConfig is null)
        {
            Console.WriteLine("An error occurred");
            Console.WriteLine("Exiting...");
            Thread.Sleep(2000);
            return;
        }
        #endregion

        #region Get Format Strategy
        Console.Write("Enter the path to the weather data file: ");
        string? weatherDataFilePath = Console.ReadLine();

        ITextFormatStrategy? textFormatStrategy = TextFormatStrategyFactory.GetTextFormatStrategy(weatherDataFilePath);

        if (textFormatStrategy is null)
        {
            Console.WriteLine("Cannot Handle that File");
            Console.WriteLine("Exiting...");
            Thread.Sleep(2000);
            return;
        }
        #endregion

        string weatherDataText = File.ReadAllText(weatherDataFilePath);
        WeatherPublisher weatherDataPublisher = new WeatherPublisher(weatherDataText, textFormatStrategy, botConfig);

        WeatherData newData = new WeatherData
        {
            Humidity = 10000,
            Location = "New Jersy",
            Temperature = 20000
        };

        weatherDataPublisher.WeatherData = newData;

    }

    private static BotConfiguration? LoadConfig()
    {
        string filePath = "config.json";
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        BotConfiguration? botConfig = ManagerConfiguration.LoadConfig(filePath, options);
        return botConfig;
    }
}

