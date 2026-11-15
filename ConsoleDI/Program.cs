// See https://aka.ms/new-console-template for more information


using ConsoleDI;
using ConsoleDI.Abstractions;
using ConsoleDI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// register services
builder.Services.AddSingleton<ILocalCache, InMemoryCache>();
builder.Services.AddSingleton<IMonitor, TemperatureMonitorer>();
builder.Services.AddScoped<ISensorReader, ConsoleSensorReader>(); //AddSingleton for GPIO/I2C
builder.Services.AddScoped<ICloudUploader, ConsoleCloudUploader>();
builder.Services.AddScoped<IEdgePipeline, EdgePipeline>();

//register background worker
builder.Services.AddHostedService<TemperatureHandler>();
builder.Services.AddHostedService<Worker>();

// build and run
using IHost host = builder.Build();
await host.RunAsync();
