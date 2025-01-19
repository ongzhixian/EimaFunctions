namespace EimaFunctions.ResponseModels;

public class ResponseBodyModel
{
    
}

public class AuthenticateUserResponse 
{
    public bool IsAuthenticated { get; set; }

    public string Username { get; set; } = string.Empty;
    
    public string Token { get; set; } = string.Empty;
}

// public record AuthenticateUserResponseRecord 
// {
//     public bool IsAuthenticated { get; set; }
//     
//     public string Username  { get; set; }
//     
//     public string Token { get; set; } = string.Empty;
// }