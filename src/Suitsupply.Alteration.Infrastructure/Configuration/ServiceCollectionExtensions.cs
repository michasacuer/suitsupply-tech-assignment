using Azure.Data.Tables;
using Azure.Identity;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using Suitsupply.Alteration.Common.Interfaces;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;
using Suitsupply.Alteration.Infrastructure.Common;
using Suitsupply.Alteration.Infrastructure.EmailSender;
using Suitsupply.Alteration.Infrastructure.MassTransit.Consumers.AlterationFinished;
using Suitsupply.Alteration.Infrastructure.MassTransit.Consumers.OrderPaid;
using Suitsupply.Alteration.Infrastructure.Repository;

namespace Suitsupply.Alteration.Infrastructure.Configuration;

internal static class ServiceCollectionExtensions
{
    internal static void AddClock(this IServiceCollection services, bool isDevelopment)
    {
        if (isDevelopment)
        {
            services.AddSingleton<IClock, DebugClock>();
        }
        else
        {
            services.AddSingleton<IClock, Clock>();
        }
    }

    internal static void AddTableStorageRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(_ =>
        {
            var tableServiceClient = new TableServiceClient(new Uri(configuration["TableStorage:Uri"]), new DefaultAzureCredential());
            tableServiceClient.CreateTableIfNotExists(configuration["TableStorage:TableName"]);
    
            return tableServiceClient.GetTableClient(configuration["TableStorage:TableName"]);
        });
        
        services.AddScoped<ICustomerRequestRepository, CustomerRequestRepository>();
    }
    
    internal static void AddSendGrindMailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSendGrid(options =>
        {
            options.ApiKey = configuration.GetConnectionString("SendGrid");
        });
        
        services.AddScoped<IEmailService, SendGridEmailService>();
    }
    
    internal static void AddMassTransitConsumers(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<AlterationFinishedConsumer>();
            x.AddConsumer<OrderPaidConsumer>();
            
            x.UsingAzureServiceBus((ctx, cfg) =>
            {
                cfg.Host(config.GetConnectionString("ServiceBus"));
                cfg.AddDeserializer(new SystemTextJsonRawMessageSerializerFactory());
                
                cfg.ReceiveEndpoint(config["ServiceBus:AlterationFinishedInputQueue"], e =>
                {
                    e.UseRawJsonDeserializer();
                    e.MaxConcurrentCalls = 1;
                    e.ConfigureConsumer<AlterationFinishedConsumer>(ctx);
                });
                
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