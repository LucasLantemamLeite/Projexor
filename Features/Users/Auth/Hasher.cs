using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Projexor.Features.Users.Auth;

public static class Hasher
{
    private readonly static int Memory = 1024 * 128;
    private readonly static int Parallelism = 5;
    private readonly static int Iterations = 4;

    public static string GenerateHash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);

        var argon = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            MemorySize = Memory,
            DegreeOfParallelism = Parallelism,
            Iterations = Iterations
        };

        var passwordHash = argon.GetBytes(32);

        return Convert.ToBase64String(salt.Concat(passwordHash).ToArray());
    }

    public static bool VerifyHash(string passwordHash, string password)
    {
        var decodifation = Convert.FromBase64String(passwordHash);

        var salt = decodifation.Take(16).ToArray();

        var storeHash = decodifation.Skip(16).ToArray();

        var argon = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            MemorySize = Memory,
            DegreeOfParallelism = Parallelism,
            Iterations = Iterations
        };

        var hash = argon.GetBytes(32);

        return CryptographicOperations.FixedTimeEquals(hash, storeHash);
    }
}