namespace Domain.Products;

public sealed class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ICollection<ProductPrice> Prices { get; private set; } = new List<ProductPrice>();
    public ProductDetail Detail { get; private set; } = default!;
    public ICollection<ProductTag> Tags { get; private set; } = new List<ProductTag>();
    public DateTime CreatedTime { get; private set; }
    public DateTime LastUpdatedTime { get; private set; }

    private Product() { }

    public static Product CreateNew(string name, string description,
        string brand, float measure, string measureUnit, string[] tags,
        DateTime CreatedTime)
    {
        var id = Guid.NewGuid();
        return new()
        {
            Id = id,
            Name = name,
            Detail = ProductDetail.CreateNew(id, description, brand, measure, measureUnit),
            Tags = [.. tags.Select(ProductTag.CreateNew)],
            CreatedTime = CreatedTime,
            LastUpdatedTime = CreatedTime,
        };
    }
}