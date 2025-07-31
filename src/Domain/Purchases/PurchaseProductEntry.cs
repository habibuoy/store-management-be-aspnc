using Domain.Common;

namespace Domain.Purchases;

public sealed class PurchaseProductEntry : ProductEntry
{
    public Guid PurchaseId { get; private set; }

    // navs
    public Purchase Purchase { get; private set; } = default!;

    private PurchaseProductEntry() { }

    public static PurchaseProductEntry CreateNew(Guid purchaseId, Guid productId, Guid priceId,
        int quantity)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            PurchaseId = purchaseId,
            ProductId = productId,
            PriceId = priceId,
            Quantity = quantity
        };
    }
}