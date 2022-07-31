using AspNetCoreRateLimit;
using MediatR;
using Microsoft.Identity.Web;
using Serilog;
using Suitsupply.Alteration.Api.Commands.SendCustomerRequest;
using Suitsupply.Alteration.Api.Dtos.Profiles;
using Suitsupply.Alteration.Api.Extensions;
using Suitsupply.Alteration.Api.Middlewares;
using Suitsupply.Alteration.Api.Services;
using Suitsupply.Alteration.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.BuildApiServices();

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddOptions();
builder.Services.AddMemoryCache();
builder.Services.AddRateLimiter(builder.Configuration);

builder.Services.AddMediatR(typeof(SendCustomerRequestCommandDto));
builder.Services.AddAutoMapper(typeof(ModelToDtoProfile));
builder.Services.AddRequestValidation();
builder.Services.AddScoped<IHttpContextFacade, HttpContextFacade>();

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.UseIpRateLimiting();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseMiddleware<ExceptionWrapperMiddleware>();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();