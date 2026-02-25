using CicloSustentavel.Domain.Security.Cryptography;

namespace CicloSustentavel.Infrastructure.Security.Cryptography;
public class EncryptBcrypt : IPasswordEncrypt
{
    public string Encrypt(string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        return passwordHash;
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
