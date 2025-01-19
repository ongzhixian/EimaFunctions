using System.Text.Json;

namespace EimaFunctions.Services;

public class JsonSerialization
{
    public static JsonSerializerOptions Options { get; set; }
    
     static JsonSerialization()
    {
        //JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        Options = new(JsonSerializerDefaults.Web);
    }

}