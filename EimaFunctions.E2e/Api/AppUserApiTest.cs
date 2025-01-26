using EimaFunctions.E2e.Utilities;
using EimaFunctions.ResponseModels;

using Microsoft.Playwright;

namespace EimaFunctions.E2e.Api;

[TestClass]
public class AppUserApiTest : PlaywrightTest
{
    [ClassInitialize]
    public static void AppUserApiTestInitialize(TestContext testContext)
    {
        // Access TestContext properties and methods here. The properties related to the test run are not available.

        //var targetEnvironment = Environment.GetEnvironmentVariable("E2E_TARGET_ENVIRONMENT");
        
        // var configuration = testContext.Properties["Configuration"];
        // var prop = testContext.Properties["MyCustomTestRunParameter"];
        // testContext.WriteLine("testContext-prop {0} {1} {2}", prop, configuration, targetEnvironment);
        //
        // //string configuration = TestContext.Properties["Configuration"].ToString();
        // Console.WriteLine("console-prop {0} {1} {2}", prop, configuration, targetEnvironment);

        //foreach (DictionaryEntry prop in testContext.Properties)
        //{
        //    Console.WriteLine("Property {0} = {1}", prop.Key, prop.Value);
        //}

        //foreach (DictionaryEntry prop in Environment.GetEnvironmentVariables())
        //{
        //    Console.WriteLine("Environment variable {0} = {1}", prop.Key, prop.Value);
        //}

//#if DEBUG
//        Console.WriteLine("IS DEBUG");
//#endif
//#if RELEASE
//        Console.WriteLine("IS RELEASE");
//#endif

        var baseUrl = TestSettings.GetSetting("functionapp:baseUrlaa");

        Console.WriteLine("baseUrl is {0}", baseUrl);

    }

    [TestMethod]
    public async Task WhenGettingPageOneOfListAppUser_ReturnsOkResponseWithFirst10AppUsers()
    {
        // Replace with your Azure Function URL
        string functionUrl = "http://localhost:7071/api/v1/app-user";

        // Prepare data for the function call (if applicable)
        // var data = new { 
        //     // ... your data here ...
        // };
        
        // IAPIRequestContext request = await Playwright.APIRequest.NewContextAsync(new()
        // {
        //     // Set the base URL for the API requests
        //     BaseURL = "https://example.com/api",
        //
        //     // Set extra HTTP headers for the API requests
        //     ExtraHTTPHeaders = new Dictionary<string, string>() 
        //     { 
        //         { "Content-Type", "application/xml" } 
        //     },
        //
        //     // Ignore HTTPS errors
        //     IgnoreHTTPSErrors = true
        // });
        IAPIRequestContext request = await Playwright.APIRequest.NewContextAsync();
        
        // Make the API call
        // var response = await request.GetAsync(functionUrl, new { 
        //     Body = JsonSerializer.Serialize(data), 
        //     Headers = new { 
        //         "Content-Type" = "application/json" 
        //     } 
        // });
        
        IAPIResponse response = await request.GetAsync(functionUrl);
        var responseText = await response.TextAsync();
        var responseObject = System.Text.Json.JsonSerializer.Deserialize<ListAppUserResponse>(responseText);
        
        Assert.IsNotNull(response);
        
        Assert.IsNotNull(responseText);
        Assert.IsNotNull(responseObject);
        Assert.IsTrue(responseObject.ListEntries.Count >= 0);
        
        
        
        
        // Act
        // var newIssue = await Request.PostAsync("/repos/" + USER + "/" + REPO + "/issues", new() { DataObject = data });
        // await Expect(newIssue).ToBeOKAsync();

        // var issues = await Request.GetAsync("/repos/" + USER + "/" + REPO + "/issues");
        // await Expect(newIssue).ToBeOKAsync();
        // var issuesJsonResponse = await issues.JsonAsync();
        //
        // JsonElement? issue = null;
        // foreach (JsonElement issueObj in issuesJsonResponse?.EnumerateArray())
        // {
        //     if (issueObj.TryGetProperty("title", out var title) == true)
        //     {
        //         if (title.GetString() == "[Feature] request 1")
        //         {
        //             issue = issueObj;
        //         }
        //     }
        // }
        
        // Asserts
        // Assert.IsNotNull(issue);
        // Assert.AreEqual("Feature description", issue?.GetProperty("body").GetString());
    }
}