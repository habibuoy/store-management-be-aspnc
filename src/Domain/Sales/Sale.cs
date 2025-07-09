namespace Domain.Sales;

public sealed class Sale
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public ICollection<SaleProductEntry> Products { get; private set; } = new List<SaleProductEntry>();
    public ICollection<SaleTag> Tags { get; private set; } = new List<SaleTag>();
    public DateTime OccurenceTime { get; private set; }
    public DateTime LastUpdatedTime { get; private set; }

    private Sale() { }

    public static Sale CreateNew(string title, string[] tags,
        SaleProductEntry[] products,
        DateTime occurenceTime, DateTime createdTime)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Products = products,
            Tags = [.. tags.Select(SaleTag.CreateNew)],
            OccurenceTime = occurenceTime,
            LastUpdatedTime = createdTime,
        };
    }
}