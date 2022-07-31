using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Suitsupply.Alteration.Infrastructure.Logger;

public static class SerilogExtensions
{
    public static void UseSerilog(this IHostBuilder app)
    {
        app.UseSerilog((ctx, loggerConfiguration) =>
            loggerConfiguration
                .ReadFrom.Configuration(ctx.Configuration)
                // .WriteTo.ApplicationInsights(new TelemetryConfiguration
                // {
                //     InstrumentationKey = ""
                // }, TelemetryConverter.Traces)
                .WriteTo.Console());
    }
}