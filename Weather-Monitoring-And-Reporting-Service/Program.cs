using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using Weather_Monitoring_And_Reporting_Service.Configuration;
using Weather_Monitoring_And_Reporting_Service.Publisher;
using Weather_Monitoring_And_Reporting_Service.Strategies;
using static Weather_Monitoring_And_Reporting_Service.Weather;

namespace Weather_Monitoring_And_Reporting_Service
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region Load Bot Configurations
                BotConfiguration? botConfig = LoadConfig();

                if (botConfig is null)
                {
                    Console.WriteLine("An error occurred while loading configurations.");
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
                    Console.WriteLine("Cannot handle that file.");
                    Console.WriteLine("Exiting...");
                    return;
                }
                #endregion

                string weatherDataText = File.ReadAllText(weatherDataFilePath);
                WeatherPublisher weatherDataPublisher = new WeatherPublisher(weatherDataText, textFormatStrategy, botConfig);

                WeatherData newData = new WeatherData
                {
                    Humidity = 10000,
                    Location = "New Jersey",
                    Temperature = 20000
                };

                weatherDataPublisher.WeatherData = newData;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Configuration file not found.");
                Console.WriteLine("Exiting...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Exiting...");
            }
        }

        private static BotConfiguration? LoadConfig()
        {
            string filePath = "config.json";

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Configuration file not found.");
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            BotConfiguration? botConfig = ManagerConfiguration.LoadConfig(filePath, options);
            return botConfig;
        }
    }
}
