using System.Security.Cryptography;
using LockManagementSystem.Application.Interface.Auth;

namespace LockManagementSystem.Infrastructure.Services.Auth;

public class PasswordService : IPasswordService
{
    private const int SaltSize = 16;

    private const int HashSize = 20;

    private const string HashKey = "#CLAY#";
    
    public string Hash(string password, int iterations)
    {
        //create salt
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

        //create hash
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
        var hash = pbkdf2.GetBytes(HashSize);
        //combine salt and hash
        var hashBytes = new byte[SaltSize + HashSize];

        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        //convert to base64
        var base64Hash = Convert.ToBase64String(hashBytes);
        //format hash with extra information
        return string.Format("{0}{1}${2}", HashKey, iterations, base64Hash);
    }

    public string Hash(string password) => Hash(password, 2000);

    private static bool IsHashSupported(string hashString) => hashString.Contains(HashKey);
    
    public bool Verify(string password, string hashedPassword)
    {
        if (!IsHashSupported(hashedPassword))
        {
            throw new NotSupportedException("The hash type is not supported");
        }

        var splitHashString = hashedPassword.Replace(HashKey, "").Split('$');
        var iterations = int.Parse(splitHashString[0]);
        var base64Hash = splitHashString[1];

        //get hash bytes
        var hashBytes = Convert.FromBase64String(base64Hash);

        //get salt
        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        //create hash with given salt
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
        var hash = pbkdf2.GetBytes(HashSize);

        //get result
        for (var i = 0; i < HashSize; i++)
        {
            if (hashBytes[i + SaltSize] != hash[i])
            {
                return false;
            }
        }
        return true;
    }
}