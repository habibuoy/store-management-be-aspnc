using Shared;

namespace Domain.Users;

public static class UserErrors
{
    public static Error NotFound(string userId) => Error.NotFound(
        "Users.NotFound",
        $"User with id '{userId}' was not found"
    );

    public static Error EmailAlreadyRegistered(string email) => Error.Conflict(
        "Users.EmailAlreadyRegistered",
        $"User with email '{email}' is already registered"
    );
}