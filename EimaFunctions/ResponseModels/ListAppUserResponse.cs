namespace EimaFunctions.ResponseModels;

public class ListAppUserResponse
{
    public IList<ListAppUserEntry> ListEntries { get; set; } = new List<ListAppUserEntry>();
}

public class ListAppUserEntry
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    public string? OAuthProvider { get; set; } = null;
    public string? DisplayName { get; set; } = null;
}