namespace Application.Products;

public sealed record ProductResponse(
    Guid Id, string Name, string? Description,
    float Measure, MeasureUnitResponse MeasureUnit, string Brand,
    decimal Price, string[] Tags, DateTime CreatedTime,
    DateTime LastUpdatedTime
);

public sealed record MeasureUnitResponse(int Id, string Name);