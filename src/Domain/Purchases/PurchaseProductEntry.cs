using Domain.Common;
using Domain.Products;

namespace Domain.Purchases;

public sealed class PurchaseProductEntry : ProductEntry
{
    // public Guid Id { get; private set; }
    public Guid PurchaseId { get; private set; }
    // public Guid ProductId { get; private set; }
    // public Guid PriceId { get; private set; }
    // public int Quantity { get; private set; }

    // navs
    public Purchase Purchase { get; private set; } = default!;
    // public Product? Product { get; private set; }
    // public ProductPrice? ProductPrice { get; private set; }

    private PurchaseProductEntry() { }

    public static PurchaseProductEntry CreateNew(Guid productId, Guid priceId,
        int quantity)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            PriceId = priceId,
            Quantity = quantity
        };
    }
}