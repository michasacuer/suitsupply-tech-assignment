using Azure.Data.Tables;
using Azure.Identity;
using MediatR;
using Microsoft.Identity.Web;
using Suitsupply.Alteration.Common.Interfaces;
using Suitsupply.Alteration.Domain.CustomerRequestAggregate;
using Suitsupply.Alteration.Infrastructure.Common;
using Suitsupply.Alteration.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration["KeyVault:Uri"]), new DefaultAzureCredential());

builder.Services.AddScoped(_ =>
{
    var tableServiceClient = new TableServiceClient(new Uri(builder.Configuration["TableStorage:Uri"]), new DefaultAzureCredential());
    tableServiceClient.CreateTableIfNotExists(builder.Configuration["TableStorage:TableName"]);
    
    return tableServiceClient.GetTableClient(builder.Configuration["TableStorage:TableName"]);
});

builder.Services.AddScoped<ICustomerRequestRepository, CustomerRequestRepository>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IClock, DebugClock>();
}
else
{
    builder.Services.AddSingleton<IClock, Clock>();
}

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();