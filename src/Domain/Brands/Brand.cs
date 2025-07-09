namespace Domain.Brands;

public sealed class Brand
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedTime { get; private set; }

    private Brand() { }

    public static Brand CreateNew(string name)
    {
        return new()
        {
            Name = name
        };
    }
}