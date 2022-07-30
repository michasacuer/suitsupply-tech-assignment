using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Suitsupply.Alteration.Infrastructure.MassTransit;

public static class MassTransitExtensions
{
    public static void AddMassTransitConsumer(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(x =>
        {
            
            x.UsingAzureServiceBus((ctx, cfg) =>
            {
                cfg.Host(config["ServiceBus:ConnectionString"]);
                cfg.AddDeserializer(new SystemTextJsonRawMessageSerializerFactory(), true);
            });
        });
    }
}