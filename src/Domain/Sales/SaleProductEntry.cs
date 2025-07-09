using Domain.Common;
using Domain.Products;

namespace Domain.Sales;

public sealed class SaleProductEntry : ProductEntry
{
    // public Guid Id { get; private set; }
    public Guid SaleId { get; private set; }
    // public Guid ProductId { get; private set; }
    // public Guid PriceId { get; private set; }
    // public int Quantity { get; private set; }

    // navs
    public Sale Sale { get; private set; } = default!;
    // public Product? Product { get; private set; }
    // public ProductPrice? ProductPrice { get; private set; }

    private SaleProductEntry() { }

    public static SaleProductEntry CreateNew(Guid productId, Guid priceId,
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