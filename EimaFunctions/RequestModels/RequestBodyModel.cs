namespace EimaFunctions.RequestModels;

public class RequestBodyModel
{
    
}

public class AuthenticateUserRequest 
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegisterUserRequest
{
    public string Username { get; set; }
}