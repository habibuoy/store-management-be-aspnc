using Shared;

namespace Domain.Brands;

public static class BrandErrors
{
    public static Error AlreadyExists(string name) => Error.Conflict(
        "Brands.AlreadyExist",
        $"Brand with name '{name}' already exists"
    );

    public static Error NotFound(int id) => Error.NotFound(
        "Brands.NotFound",
        $"Brand with id '{id}' was not found"
    );
}