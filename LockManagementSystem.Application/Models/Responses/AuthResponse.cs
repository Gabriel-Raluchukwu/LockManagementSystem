namespace LockManagementSystem.Application.Models.Responses;

public class AuthResponse
{
    public string Token { get; set; }

    public DateTime CreatedAt { get; set; }

    public long ValidFor { get; set; }
}

public class SignUpResponse : AuthResponse
{
    
}

public class SignInResponse : AuthResponse
{
    
}