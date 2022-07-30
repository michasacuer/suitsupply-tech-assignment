using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suitsupply.Alteration.Infrastructure.MassTransit.Consumers.OrderPaid;

namespace Suitsupply.Alteration.Infrastructure.MassTransit;

public static class MassTransitExtensions
{
    public static void AddMassTransitOrderPaidConsumer(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderPaidConsumer>();
            
            x.UsingAzureServiceBus((ctx, cfg) =>
            {
                cfg.Host(config.GetConnectionString("ServiceBus"));
                cfg.AddDeserializer(new SystemTextJsonRawMessageSerializerFactory());
                
                cfg.SubscriptionEndpoint<OrderPaidMessage>(config["ServiceBus:OrderPaidSubscriptionName"], e =>
                {
                    e.UseRawJsonDeserializer();
                    e.MaxConcurrentCalls = 1;
                    e.ConfigureConsumer<OrderPaidConsumer>(ctx);
                });
            });
        });
    }
}