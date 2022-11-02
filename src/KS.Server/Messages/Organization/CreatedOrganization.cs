using KS.Domain.Entities;
using KS.Server.Interfaces;

namespace KS.Server.Messages;

internal class CreatedOrganization : IMessage
{
    public Organization Organization { get; set; }
    public Guid From { get; set; }
}
