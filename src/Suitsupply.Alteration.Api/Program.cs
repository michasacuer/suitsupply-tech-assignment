using MediatR;
using Microsoft.Identity.Web;
using Serilog;
using Suitsupply.Alteration.Api.Middlewares;
using Suitsupply.Alteration.Api.Services;
using Suitsupply.Alteration.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.BuildApiServices();

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IHttpContextFacade, HttpContextFacade>();

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseMiddleware<ExceptionWrapperMiddleware>();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();