using System;
using System.Text.Json;
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
                Console.WriteLine("Enter the weather data in the following format:");
                Console.WriteLine("Location: ");
                string location = Console.ReadLine();
                Console.WriteLine("Temperature: ");
                double temperature = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Humidity: ");
                double humidity = Convert.ToDouble(Console.ReadLine());

                WeatherData newData = new WeatherData
                {
                    Location = location,
                    Temperature = temperature,
                    Humidity = humidity
                };

                ITextFormatStrategy? textFormatStrategy = new JsonFormatStrategy(); 

                #endregion

                WeatherPublisher weatherDataPublisher = new WeatherPublisher(newData, textFormatStrategy, botConfig);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Exiting...");
            }
        }

        private static BotConfiguration? LoadConfig()
        {
            string filePath = "C:\\Users\\hp\\Desktop\\C#\\Weather-Monitoring-And-Reporting-Service\\config.json";


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
