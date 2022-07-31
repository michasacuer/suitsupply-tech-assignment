using System.Reflection;
using FluentValidation;

namespace Suitsupply.Alteration.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddFluentValidator(this IServiceCollection services, Type validator)
    {
        var assembliesToRegister = new List<Assembly>() { validator.Assembly };
        AssemblyScanner.FindValidatorsInAssemblies(assembliesToRegister).ForEach(pair =>
        {
            services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
        });
    }
}