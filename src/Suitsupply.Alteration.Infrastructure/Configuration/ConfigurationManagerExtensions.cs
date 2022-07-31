using System.Reflection;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Suitsupply.Alteration.Infrastructure.Configuration;

public static class ConfigurationManagerExtensions
{
    internal static void AddKeyVault(this ConfigurationManager configurationManager, IConfiguration config)
    {
        configurationManager.AddAzureKeyVault(new Uri(config["KeyVault:Uri"]), new DefaultAzureCredential());
    }
    
    internal static void AddLinkedAppsettings(this ConfigurationManager configurationManager)
    {
        configurationManager
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json", false, true);
    }
}