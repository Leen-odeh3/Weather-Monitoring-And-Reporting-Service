
using System.Text.Json;

namespace Weather_Monitoring_And_Reporting_Service.Configuration;

public class ManagerConfiguration
{
    public static BotConfiguration? LoadConfig(string filePath, JsonSerializerOptions options)
    {
        string fileContent = File.ReadAllText(filePath);
        BotConfiguration? configuration = JsonSerializer.Deserialize<BotConfiguration>(fileContent, options);
        return configuration;
    }
}
