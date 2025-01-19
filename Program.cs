using System.Text.Json;
using System.Text.Json.Serialization;
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

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// builder.Services.AddSingleton(sp => new JsonSerializerOptions
//     {
//         PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//     }
// );

// builder.Services.Configure<JsonSerializerOptions>(options =>
// {
//     
//     // options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
//     // options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//     // options.JsonSerializerOptions.WriteIndented = false;
//     // options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Default;
//     // options.JsonSerializerOptions.AllowTrailingCommas = true;
//     // options.JsonSerializerOptions.MaxDepth = 3;
//     // options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
// });

// builder.Services.ConfigureHttpJsonOptions(options =>
// {
//     options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
//     options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
// });

builder.Services.AddScoped<AppUserRepository>();

builder.Build().Run();
