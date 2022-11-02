using KS.Domain.Commands;
using KS.Server.Interfaces;

namespace KS.Server.Messages;

internal record ChangeOrganizationName : IOrganizationMessage<ChangeOrganizationNameCmd>
{
    public ChangeOrganizationName(ChangeOrganizationNameCmd message, Guid from)
    {      
        Message = message;
        From = from;
    }

    public ChangeOrganizationNameCmd Message { get; set; }
    public Guid From { get; set; }
}
