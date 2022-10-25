using KS.Server.Interfaces;

namespace KS.Server.Messages;

public class DeleteOrganization : IOrganizationMessage<Guid>
{
    public Guid Message { get; set; }
    public Guid From { get; set; }
}
