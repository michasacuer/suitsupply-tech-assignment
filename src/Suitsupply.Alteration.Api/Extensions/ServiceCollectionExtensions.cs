using System.Reflection;
using AspNetCoreRateLimit;
using FluentValidation;
using MediatR;
using Suitsupply.Alteration.Api.Commands.SendCustomerRequest;
using Suitsupply.Alteration.Api.PipelineBehaviours;

namespace Suitsupply.Alteration.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRequestValidation(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddFluentValidator(typeof(SendCustomerRequestCommandValidator));
    }

    public static void AddRateLimiter(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<IpRateLimitOptions>(config.GetSection("IpRateLimiting"));
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddInMemoryRateLimiting();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }
    
    private static void AddFluentValidator(this IServiceCollection services, Type validator)
    {
        var assembliesToRegister = new List<Assembly>() { validator.Assembly };
        AssemblyScanner.FindValidatorsInAssemblies(assembliesToRegister).ForEach(pair =>
        {
            services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
        });
    }
}