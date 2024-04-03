using System.Text.Json;
using Weather_Monitoring_And_Reporting_Service;
using Weather_Monitoring_And_Reporting_Service.Configuration;
using Weather_Monitoring_And_Reporting_Service.Publisher;

#region Load Bot Configurations

var botConfig = LoadConfig();

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
var weatherDataFilePath = Console.ReadLine();

var textFormatStrategy = TextFormatStrategyFactory.GetTextFormatStrategy(weatherDataFilePath);

if (textFormatStrategy == null)

{
    Console.WriteLine("Cannot Handle that File");
    Console.WriteLine("Exiting...");
    Thread.Sleep(2000);
    return;
}

#endregion

var weatherDataText = File.ReadAllText(weatherDataFilePath);
var weatherDataPublisher = new WeatherPublisher(weatherDataText, textFormatStrategy, botConfig);

var newData = new Weather
{
    Humidity = 85.0,
    Location = "Alex",
    Temperature = 23.0
};

weatherDataPublisher.WeatherData = newData;


BotConfiguration? LoadConfig()
{
    var filePath = "config.json";
    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    var botConfig = ManagerConfiguration.LoadConfig(filePath, options);
    return botConfig;
}