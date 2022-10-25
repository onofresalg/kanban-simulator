using KS.Domain.Entities;
using KS.Server.Interfaces;

namespace KS.Server.Messages;

internal class RetrievedOrganizations : IMessage
{
    public List<Organization> Organizations { get; set; }
    public Guid From { get; set; }
}
