using System.Net;
using System.Threading.Tasks;
using EimaFunctions.Api;
using EimaFunctions.Repositories;
using EimaFunctions.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
namespace EimaFunctions.Tests.Api;

[TestClass]
public class AppUserApiTest
{
    private readonly ILogger<AppUserApi> logger;
    private readonly IAppUserRepository appUserRepository;

    public AppUserApiTest()
    {
        logger = Substitute.For<ILogger<AppUserApi>>();
        appUserRepository = Substitute.For<IAppUserRepository>();
    }
    
    [TestMethod]
    public async Task ListAppUser()
    {
        appUserRepository.GetUserList(Arg.Any<int>(), Arg.Any<byte>()).Returns([
            new () { Username = "ad", OAuthProvider = "", Email = "email@email.com", DisplayName = ""},
            new () { Username = "ad", OAuthProvider = "", Email = "email@email.com", DisplayName = ""},
            new () { Username = "ad", OAuthProvider = "", Email = "email@email.com", DisplayName = ""},
        ]);
        var api = new AppUserApi(logger, appUserRepository);

        var httpContext = new DefaultHttpContext();
        var request = httpContext.Request;
        

        var response = await api.ListAppUser(request);
        //     new ListAppUserRequest()
        // {
        //     Page = 1,
        // });

        await appUserRepository.Received(1).GetUserList(Arg.Any<int>(), Arg.Any<byte>());
        Assert.IsNotNull(response);
        Assert.IsInstanceOfType<OkObjectResult>(response);
        
        var okObjectResult = (OkObjectResult)response;
        Assert.AreEqual((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        
        Assert.IsInstanceOfType<ListAppUserResponse>(okObjectResult.Value);
        var responseObject = (ListAppUserResponse)okObjectResult.Value;
        Assert.AreEqual(3, responseObject.ListEntries.Count);
    }
}