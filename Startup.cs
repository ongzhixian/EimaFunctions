//using EimaFunctions;
//using EimaFunctions.Repositories;

//using Microsoft.Azure.Functions.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection;

//using MongoDB.Driver;

//[assembly: FunctionsStartup(typeof(Startup))]

//namespace EimaFunctions;

//public class Startup : FunctionsStartup
//{
//    public override void Configure(IFunctionsHostBuilder builder)
//    {
//        var configuration = builder.GetContext().Configuration;

//        builder.Services.AddHttpClient();

//        //builder.Services.AddSingleton<IMyService>((s) =>
//        //{
//        //    return new MyService();
//        //});

//        //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();

//        builder.Services.AddKeyedScoped<IMongoClient>("WareLogixMongoDb", (sp, key) =>
//        {
//            return new MongoClient(configuration[$"ConnectionStrings:{key}"]);
//        });

//        builder.Services.AddKeyedScoped<IMongoDatabase>("minitools", (sp, key) =>
//        {
//            var mongoClient = sp.GetRequiredKeyedService<IMongoClient>("WareLogixMongoDb");
//            return mongoClient.GetDatabase((string)key);
//        });


//        builder.Services.AddScoped<AppUserRepository>();

//    }
//}