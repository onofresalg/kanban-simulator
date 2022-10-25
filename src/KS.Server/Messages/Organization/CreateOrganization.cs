using KS.Domain.Entities;
using KS.Server.Interfaces;

namespace KS.Server.Messages;

internal class CreateOrganization : IOrganizationMessage<Organization>
{
    public Organization Message { get; set; }
    public Guid From { get; set; }
}
