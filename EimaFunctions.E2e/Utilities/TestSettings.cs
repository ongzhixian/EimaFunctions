using Microsoft.Extensions.Configuration;

namespace EimaFunctions.E2e.Utilities;

public static class TestSettings
{
    public static IConfigurationRoot Configuration { get; private set; }

    static TestSettings()
    {
        var environmentName = Environment.GetEnvironmentVariable("TARGET_ENVIRONMENT") ?? string.Empty;

        if (environmentName == null)
        {
            Console.WriteLine("WARNING: Environment variable for TARGET_ENVIRONMENT not defined; Default to 'Development'.");
            environmentName = "Development";
        }

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("testsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"testsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        Configuration = builder.Build();
    }

    public static string? GetSetting(string key)
    {
        var settingValue = Configuration[key];

        if (settingValue == null) Console.WriteLine("WARNING: TestSetting {0} not defined.", key);

        return settingValue;
    }
    
}
