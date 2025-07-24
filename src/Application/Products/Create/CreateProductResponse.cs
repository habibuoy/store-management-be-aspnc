namespace Application.Products.Create;

public sealed record CreateProductResponse(
    Guid Id, string Name, string? Description,
    float Measure, string Unit, string Brand,
    string Price, string[] Tags, DateTime CreatedTime,
    DateTime LastUpdatedTime
);