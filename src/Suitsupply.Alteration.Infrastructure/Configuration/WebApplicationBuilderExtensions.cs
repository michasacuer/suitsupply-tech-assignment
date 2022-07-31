using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Suitsupply.Alteration.Infrastructure.Configuration;

public static class WebApplicationBuilderExtensions
{
    public static void BuildApiServices(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilogFromConfigurationFile();
        builder.Configuration.AddKeyVault(builder.Configuration);
        builder.Services.AddTableStorageRepository(builder.Configuration);
        builder.Services.AddMassTransitForSender(builder.Configuration);
        builder.Services.AddClock(builder.Environment.IsDevelopment());
    }

    public static void BuildWebJobServices(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddLinkedAppsettings();
        builder.Configuration.AddKeyVault(builder.Configuration);
        builder.Host.UseSerilogFromConfigurationFile();
        builder.Services.AddSendGrindMailService(builder.Configuration);
        builder.Services.AddTableStorageRepository(builder.Configuration);
        builder.Services.AddClock(builder.Environment.IsDevelopment());
    }

    public static void AddMassTransitConsumers(this WebApplicationBuilder builder)
        => builder.Services.AddMassTransitConsumers(builder.Configuration);
}