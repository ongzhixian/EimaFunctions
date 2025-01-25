using System.Collections.Specialized;

namespace EimaFunctions.RequestModels;

public record ListAppUserRequest
{
    public ListAppUserRequest(NameValueCollection query)
    {
        if (query.AllKeys.Contains("page", StringComparer.OrdinalIgnoreCase))
        {
            if (int.TryParse(query["page"], out int pageNumber)) Page = pageNumber;
            
        }
    }

    public ListAppUserRequest()
    {
    }

    public int Page { get; set; } = 1;
}