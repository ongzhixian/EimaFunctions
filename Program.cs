using EimaFunctions.Repositories;

using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MongoDB.Driver;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();


var configuration = builder.Configuration;

builder.Services.AddKeyedScoped<IMongoClient>("WareLogixMongoDb", (sp, key) =>
{
    return new MongoClient(configuration[$"ConnectionStrings:{key}"]);
});

builder.Services.AddKeyedScoped<IMongoDatabase>("minitools", (sp, key) =>
{
    var mongoClient = sp.GetRequiredKeyedService<IMongoClient>("WareLogixMongoDb");
    return mongoClient.GetDatabase((string)key);
});


builder.Services.AddScoped<AppUserRepository>();

builder.Build().Run();
