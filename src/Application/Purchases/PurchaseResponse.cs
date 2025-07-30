namespace Application.Purchases;

public sealed record PurchaseResponse(
    Guid Id, string Title, string[] Tags,
    ProductEntryResponse[] ProductEntries,
    decimal TotalPrice,
    DateTime OccurrenceTime, DateTime CreatedTime,
    DateTime LastUpdatedTime
);

public sealed record ProductEntryResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    Guid PriceId,
    decimal PriceValue,
    int Quantity,
    decimal TotalPrice
);