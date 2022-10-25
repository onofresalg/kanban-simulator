using KS.Domain.ValueObject;

namespace KS.Domain.Entities;

public class GenEntity
{
    public Guid Id { get; set; }
    public Audit Audit { get; set; }
}
