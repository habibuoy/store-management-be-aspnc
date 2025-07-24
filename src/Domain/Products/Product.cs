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

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdateDetail(ProductDetail detail)
    {
        Detail = detail;
    }

    public void UpdateTags(List<ProductTag> tags)
    {
        Tags = tags;
    }

    public void AddPrice(ProductPrice price)
    {
        Prices.Add(price);
    }

    public void UpdateLastUpdatedTime(DateTime dateTime)
    {
        LastUpdatedTime = dateTime;
    }

    public static Product CreateNew(string name, string? description,
        int brandId, float measure, int measureUnitId, ProductTag[] tags,
        DateTime CreatedTime)
    {
        var id = Guid.NewGuid();
        return new()
        {
            Id = id,
            Name = name,
            Detail = ProductDetail.CreateNew(id, description, brandId, measure, measureUnitId),
            Tags = tags,
            CreatedTime = CreatedTime,
            LastUpdatedTime = CreatedTime,
        };
    }
}