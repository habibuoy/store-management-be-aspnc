using Domain.Products;

namespace Domain.Common;

public class ProductEntry
{
    public Guid Id { get; protected set; }
    public Guid ProductId { get; protected set; }
    public Guid PriceId { get; protected set; }
    public int Quantity { get; protected set; }

    // navs
    public Product? Product { get; protected set; }
    public ProductPrice? ProductPrice { get; protected set; }

    protected ProductEntry() { }
}