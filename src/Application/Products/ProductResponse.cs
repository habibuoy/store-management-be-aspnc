using Domain.Products;

namespace Application.Products;

public sealed record ProductResponse(
    Guid Id, string Name, string? Description,
    float Measure, ProductUnitResponse MeasureUnit, string Brand,
    decimal Price, string[] Tags, DateTime CreatedTime,
    DateTime LastUpdatedTime
);

public sealed record ProductUnitResponse(int Id, string Name);