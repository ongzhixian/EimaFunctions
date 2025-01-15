using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EimaFunctions.Repositories;

namespace EimaFunctions;

public class HttpExample
{
    private readonly ILogger<HttpExample> logger;
    private readonly string connectionString;

    private readonly AppUserRepository appUserRepository;

    public HttpExample(ILogger<HttpExample> logger, AppUserRepository appUserRepository)
    {
        this.logger = logger;
        this.appUserRepository = appUserRepository; ;
        //connectionString = configuration["ConnectionStrings:WareLogixMongoDb"] ?? "MISSINE";
    }

    [Function("HttpExample")]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var appUser = await appUserRepository.GetUserAsync("ZHIXIAN");

        return new OkObjectResult($"Welcome to Azure Functions! {appUser.Username}");
    }
}
