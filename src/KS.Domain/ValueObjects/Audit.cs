namespace KS.Domain.ValueObject;

public record Audit
{
    public DateTime Created { get; init; } = DateTime.Now;
    public DateTime Updated { get; init; } = DateTime.Now;
}