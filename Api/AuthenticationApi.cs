using System.Net;
using EimaFunctions.RequestModels;
using EimaFunctions.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace EimaFunctions.Api;

public class AuthenticationApi(ILogger<AuthenticationApi> logger)
{
    [Function("AuthenticateUser")]
    [OpenApiOperation(operationId: "AuthenticateUser")]
    // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiRequestBody("application/json", typeof(AuthenticateUserRequest), Description = "JSON request body containing { username, password }")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response message containing a JSON result.")]
    public async Task<IActionResult> PostAuthenticateUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "authenticate")] 
        [Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute] 
        AuthenticateUserRequest authenticateUserRequest)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");
        
        //var content = await new StreamReader(req.Body).ReadToEndAsync();
        
         //var authenticateUserRequest2 = await JsonSerializer.DeserializeAsync<AuthenticateUserRequest>(req.Body, JsonSerialization.Options);
        //
        // if (authenticateUserRequest == null) return new BadRequestResult();
        
        logger.LogInformation("Returns OK with AuthenticateUserResponse {username}", authenticateUserRequest.Username);
        return new OkObjectResult(new AuthenticateUserResponse
        {
            IsAuthenticated = true,
            Username = authenticateUserRequest.Username,
            Token = $"SomeAuthTokenXxx{authenticateUserRequest.Username}",
        });
    }
}