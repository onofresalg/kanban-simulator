using KS.Domain.Entities;
using KS.Server.Interfaces;
using KS.Server.Servers;

namespace KS.Server;

public interface IEventBus
{
    void Register(IGenServer server);
    void Unregister(IGenServer server);
    Task Communicate(Guid To, IMessage message);
}
