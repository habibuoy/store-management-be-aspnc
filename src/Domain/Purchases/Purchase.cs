namespace Domain.Purchases;

public sealed class Purchase
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public ICollection<PurchaseProductEntry> Products { get; private set; } = new List<PurchaseProductEntry>();
    public ICollection<PurchaseTag> Tags { get; private set; } = new List<PurchaseTag>();
    public DateTime OccurenceTime { get; private set; }
    public DateTime LastUpdatedTime { get; private set; }

    private Purchase() { }

    public static Purchase CreateNew(string title, string[] tags,
        PurchaseProductEntry[] products,
        DateTime occurenceTime, DateTime createdTime)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Products = products,
            Tags = [.. tags.Select(PurchaseTag.CreateNew)],
            OccurenceTime = occurenceTime,
            LastUpdatedTime = createdTime,
        };
    }
}