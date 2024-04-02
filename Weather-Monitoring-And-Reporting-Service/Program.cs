using System.Text.Json;
using System.Xml.Serialization;
using Weather_Monitoring_And_Reporting_Service.Configuration;
using Weather_Monitoring_And_Reporting_Service.Publisher;
using Weather_Monitoring_And_Reporting_Service.Strategies;

namespace Weather_Monitoring_And_Reporting_Service
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                BotConfiguration? botConfig = LoadConfig();

                if (botConfig is null)
                {
                    Console.WriteLine("An error occurred while loading configurations.");
                    Console.WriteLine("Exiting...");
                    await Task.Delay(2000);
                    return;
                }

                
                Console.WriteLine("Enter weather data in JSON or XML format:");
                string weatherDataInput = Console.ReadLine();

                Weather weatherData;
                if (weatherDataInput.StartsWith("{"))
                {
                    weatherData = JsonSerializer.Deserialize<Weather>(weatherDataInput);
                }
                else if (weatherDataInput.StartsWith("<"))
                {
                    var serializer = new XmlSerializer(typeof(Weather));
                    using (var reader = new StringReader(weatherDataInput))
                    {
                        weatherData = (Weather)serializer.Deserialize(reader);
                    }
                }
                else
                {
                    throw new FormatException("Invalid input format.");
                }

                ITextFormatStrategy? textFormatStrategy = GetTextFormatStrategy(weatherDataInput);
                WeatherPublisher weatherDataPublisher = new WeatherPublisher(weatherData, textFormatStrategy, botConfig);
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

        private static ITextFormatStrategy? GetTextFormatStrategy(string input)
        {
            if (input.StartsWith("{"))
            {
                return new JsonFormatStrategy();
            }
            else if (input.StartsWith("<"))
            {
                return new XmlFormatStrategy();
            }
            else
            {
                return null;
            }
        }
    }
}
