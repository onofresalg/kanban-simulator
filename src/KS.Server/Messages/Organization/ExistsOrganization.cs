using KS.Domain.Entities;
using KS.Server.Interfaces;

namespace KS.Server.Messages;

internal class ExistsOrganization : IMessage
{
    public Exception Error { get; set; }
    public Guid From { get; set; }
}
