using Suitsupply.Alteration.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.BuildWebJobServices();
builder.AddMassTransitConsumers();

var app = builder.Build();

app.Run();