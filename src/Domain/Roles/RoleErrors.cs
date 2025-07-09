using Shared;

namespace Domain.Roles;

public static class RoleErrors
{
    public static Error NotFound(int id, string? name) => Error.NotFound(
        "Roles.NotFound",
        $"Role with id {id} {(string.IsNullOrEmpty(name) ? string.Empty : $"({name})")} was not found."
    );

    public static Error AlreadyExist(string name) => Error.Conflict(
        "Roles.AlreadyExist",
        $"Role with name {name} already exists"
    );
}