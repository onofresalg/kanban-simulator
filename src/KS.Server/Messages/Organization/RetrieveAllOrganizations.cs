using KS.Domain.Entities;
using KS.Server.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("KS.Test.Server")]
namespace KS.Server.Messages;

internal class RetrieveAllOrganizations : IMessage
{
    public Guid From { get; set; }
}
