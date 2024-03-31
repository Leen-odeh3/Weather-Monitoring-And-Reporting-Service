using System.Text.Json;
using Weather_Monitoring_And_Reporting_Service.Configuration;

namespace Weather_Monitoring_And_Reporting_Service;

internal class Program
{
    static void Main(string[] args)
    {
        string filePath = "config.json";
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };


        BotConfiguration? BotConfiguration = ManagerConfiguration.LoadConfig(filePath, options);

        if (BotConfiguration is null)
        {
            Console.WriteLine("An error occurred");
            Console.WriteLine("Exiting...");
            Thread.Sleep(2000);
            return;
        }

        Console.WriteLine(JsonSerializer.Serialize(BotConfiguration, options));

    }
}

