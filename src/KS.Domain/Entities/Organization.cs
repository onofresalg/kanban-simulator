namespace KS.Domain.Entities;

public class Organization : GenEntity, ICloneable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, object> Simulations { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
