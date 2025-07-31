using Domain.Common;

namespace Domain.Sales;

public sealed class SaleProductEntry : ProductEntry
{
    public Guid SaleId { get; private set; }

    // navs
    public Sale Sale { get; private set; } = default!;

    private SaleProductEntry() { }

    public static SaleProductEntry CreateNew(Guid saleId, Guid productId, Guid priceId,
        int quantity)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            SaleId = saleId,
            ProductId = productId,
            PriceId = priceId,
            Quantity = quantity
        };
    }
}