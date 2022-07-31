namespace LockManagementSystem.Application.Interface.Auth;

public interface IPasswordService
{
    public string Hash(string password, int iterations);

    public string Hash(string password);

    public bool Verify(string password, string hashedPassword);
}