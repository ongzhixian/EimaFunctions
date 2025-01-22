using System.Net;
using System.Web.Http;

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

public class ArtemisApi(ILogger<ArtemisApi> logger)
{
    [Function("ArtemisActionHandler")]
    [OpenApiOperation(operationId: "ArtemisActionHandler")]
    //[OpenApiRequestBody("application/json", typeof(RegisterUserRequest))]
    [OpenApiRequestBody("application/json", typeof(object))]
    //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response message containing a JSON result.")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "slack-artemis")]
        [Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute]
        SlackEvent slackEvent)
    {
        //var content = await new StreamReader(req.Body).ReadToEndAsync();
        ////var urlVerificationRequest = System.Text.Json.JsonSerializer.Deserialize<UrlVerificationRequest>(content, JsonSerialization.Options);

        //var urlVerificationRequest = await System.Text.Json.JsonSerializer.DeserializeAsync<SlackMessage>(req.Body);

        ////var urlVerificationRequest = await System.Text.Json.JsonSerializer.DeserializeAsync<SlackUrlVerificationRequest>(req.Body, JsonSerialization.Options);

        //if (urlVerificationRequest == null) return new BadRequestResult();

        //return new OkObjectResult(new SlackUrlVerificationResponse
        //{
        //    //Challenge = urlVerificationRequest.Challenge
        //    Challenge = "a"
        //});

        if (slackEvent == null || string.IsNullOrEmpty(slackEvent.Type))
        {
            return new BadRequestResult();
            //return new BadRequestResult("Invalid Slack event received.");
        }

        try
        {
            // Verify the request authenticity (if needed)
            // 1. Check for the Slack signature header
            // 2. Calculate the signature using your signing secret
            // 3. Compare the calculated signature with the received signature

            switch (slackEvent.Type)
            {
                case "url_verification":
                    // Respond with challenge token for URL verification
                    return new OkObjectResult(new { challenge = slackEvent.Challenge });
                case "event_callback":
                    // Handle different event types
                    var innerEvent = slackEvent.Event;
                    switch (innerEvent.Type)
                    {
                        case "message":
                            // Handle message events
                            if (innerEvent.User != null && innerEvent.Text != null)
                            {
                                // Process the message (e.g., respond, store data)
                                Console.WriteLine($"Received message from user {innerEvent.User}: {innerEvent.Text}");
                                // You can use a library like SlackClient to interact with Slack API
                            }
                            break;
                        case "app_mention":
                            // Handle app mentions
                            Console.WriteLine($"App mentioned in message: {innerEvent.Text}");
                            // Respond to the mention
                            break;
                            // Handle other event types as needed
                    }
                    break;
                default:
                    // Handle unknown event types
                    break;
            }

            return new OkResult(); // Acknowledge receipt of the event
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Slack event: {ex.Message}");
            return new ExceptionResult(ex, false);
        }
    }
}