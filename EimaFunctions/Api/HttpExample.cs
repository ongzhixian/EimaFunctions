using System.Net;
using EimaFunctions.Repositories;
using EimaFunctions.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace EimaFunctions.Api;

public class HttpExample
{
    private readonly ILogger<HttpExample> logger;
    // private readonly string connectionString;
    private readonly AppUserRepository appUserRepository;

    public HttpExample(ILogger<HttpExample> logger, AppUserRepository appUserRepository)
    {
        this.logger = logger;
        this.appUserRepository = appUserRepository; ;
        //connectionString = configuration["ConnectionStrings:WareLogixMongoDb"] ?? "MISSINE";
    }

    [Function("HttpExample")]
    [OpenApiOperation(operationId: "Run")]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiRequestBody("application/json", typeof(RequestBodyModel), Description = "JSON request body containing { hours, capacity}")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response message containing a JSON result.")]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var appUser = await appUserRepository.GetUserAsync("ZHIXIAN");

        return new OkObjectResult($"Welcome to Azure Functions! {appUser.Username}");
    }
}
