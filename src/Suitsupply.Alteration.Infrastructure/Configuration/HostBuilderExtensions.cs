using Microsoft.Extensions.Hosting;
using Serilog;

namespace Suitsupply.Alteration.Infrastructure.Configuration;

internal static class HostBuilderExtensions
{
    internal static void UseSerilogFromConfigurationFile(this IHostBuilder host)
    {
        host.UseSerilog((ctx, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(ctx.Configuration));
    }
}