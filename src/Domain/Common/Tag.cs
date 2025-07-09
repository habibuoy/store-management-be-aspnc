namespace Domain.Common;

public class Tag
{
    public int Id { get; protected set; }
    public Name Name { get; protected set; } = default!;

    protected Tag() { }
}