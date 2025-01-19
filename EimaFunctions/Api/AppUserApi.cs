using System.Net;
using EimaFunctions.DbModels;
using EimaFunctions.RequestModels;
using EimaFunctions.ResponseModels;
using EimaFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace EimaFunctions.Api;

public class AppUserApi(ILogger<AppUser> logger)
{
    [Function("RegisterAppUser")]
    [OpenApiOperation(operationId: "RegisterAppUser")]
    // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiRequestBody("application/json", typeof(RegisterUserRequest), Description = "JSON request body containing { username, password }")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response message containing a JSON result.")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "app-user/")] HttpRequest req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");
        
        //var content = await new StreamReader(req.Body).ReadToEndAsync();
        
        var authenticateUserRequest = await System.Text.Json.JsonSerializer.DeserializeAsync<AuthenticateUserRequest>(req.Body, JsonSerialization.Options);

        if (authenticateUserRequest == null) return new BadRequestResult();
        
        logger.LogInformation("Returns OK with AuthenticateUserResponse {username}", authenticateUserRequest.Username);
        return new OkObjectResult(new AuthenticateUserResponse
        {
            IsAuthenticated = true,
            Username = authenticateUserRequest.Username,
            Token = $"SomeAuthTokenXxx{authenticateUserRequest.Username}",
        });
    }
}