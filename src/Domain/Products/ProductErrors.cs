using Shared;

namespace Domain.Products;

public static class ProductErrors
{
    public static Error NotFound(Guid id) => Error.NotFound(
        "Products.NotFound",
        $"Product with id '{id}' was not found."
    );

    public static Error AlreadyExists(string name, string brand, int brandId) => Error.Conflict(
        "Products.AlreadyExist",
        $"Product with name '{name}' and brand '{brand} (id: {brandId})' already exists"
    );

    public static Error PriceNotFound(Guid id) => Error.NotFound(
        "Products.PriceNotFound",
        $"Product price with id '{id}' was not found."
    );

    public static Error UnitNotFound(int id) => Error.NotFound(
        "Products.UnitNotFound",
        $"Product unit with id '{id}' was not found."
    );

    public static Error UnitNotFound(string name) => Error.NotFound(
        "Products.UnitNotFound",
        $"Product unit with name '{name}' was not found."
    );
}