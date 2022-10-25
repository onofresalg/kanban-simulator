namespace KS.Domain.Entities;

public class Organization : GenEntity
{
    public string Name { get; set; }
    public Dictionary<string, object> Simulations { get; set; }

    public Organization()
    {

    }
}
