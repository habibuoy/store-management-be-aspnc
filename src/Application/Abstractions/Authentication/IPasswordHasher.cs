using Shared;

namespace Application.Abstractions.Authentication;

public interface IPasswordHasher
{
    string Hash(string password);

    Result Verify(string inputPassword, string hashedPassword);
}