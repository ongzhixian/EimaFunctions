using System.IO;
using System.Net;
using System.Threading.Tasks;
using EimaFunctions.Api;
using EimaFunctions.RequestModels;
using EimaFunctions.ResponseModels;
using EimaFunctions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace EimaFunctions.Tests.Api;

[TestClass]
public class AuthenticationApiTests
{
    private readonly ILogger<AuthenticationApi> logger = Substitute.For<ILogger<AuthenticationApi>>();
    
    [TestMethod]
    public async Task PostAuthenticateUserTest()
    {
        //var request = GetHttpRequest();
        AuthenticateUserRequest authenticateUserRequest = new()
        {
            Username = "username",
            Password = "password"
        };

        var api = new AuthenticationApi(logger);
        var response = await api.PostAuthenticateUser(authenticateUserRequest);

        Assert.IsNotNull(response);
        Assert.IsInstanceOfType<Microsoft.AspNetCore.Mvc.OkObjectResult>(response);
        
        var okObjectResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)response;
        Assert.AreEqual((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        Assert.IsInstanceOfType<AuthenticateUserResponse>(okObjectResult.Value);
        AuthenticateUserResponse responseObject = (AuthenticateUserResponse)okObjectResult.Value;
        
        Assert.AreEqual(true, responseObject.IsAuthenticated);
        // Assert.AreEqual("", responseObject.Username);
        // Assert.AreEqual("", responseObject.Token);
    }

    private static HttpRequest GetHttpRequest()
    {
        AuthenticateUserRequest authenticateUserRequest = new()
        {
            Username = "username",
            Password = "password"
        };
        var json = System.Text.Json.JsonSerializer.Serialize(authenticateUserRequest, JsonSerialization.Options);
        using var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
        
        var httpContext = new DefaultHttpContext();
        var request = httpContext.Request;
        request.Body = memoryStream;
        request.ContentType = "application/json";
        request.Method = System.Net.Http.HttpMethod.Post.Method;
        return request;
    }
}