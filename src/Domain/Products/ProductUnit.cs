using Domain.Common;

namespace Domain.Products;

public sealed class ProductUnit
{
    public int Id { get; private set; }
    public Name Name { get; private set; } = default!;

    // navs
    public ICollection<ProductDetail> Products { get; private set; } = new List<ProductDetail>();

    private ProductUnit() { }

    public static ProductUnit CreateNew(string name)
    {
        return new()
        {
            Name = Name.CreateNew(name)
        };
    }
}