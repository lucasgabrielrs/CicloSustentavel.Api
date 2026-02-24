using CicloSustentavel.Domain.Security.Cryptography;

namespace CicloSustentavel.Infrastructure.Security;
public class EncryptBcrypt : IPasswordEncrypt
{
    public string Encrypt(string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        return passwordHash;
    }
}
