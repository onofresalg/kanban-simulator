using KS.Domain.Entities;
using KS.Server.Interfaces;
using KS.Server.Servers;

namespace KS.Server;

public class EventBus : IEventBus
{
    private Dictionary<Guid, IGenServer> _servers;

    public EventBus()
    {
        _servers = new Dictionary<Guid, IGenServer>();
    }

    public async Task Communicate(Guid id, IMessage message)
    {
        if (_servers.TryGetValue(id, out IGenServer server))
        {
            await server.ReceiveAsync(message);
        }
    }

    public void Register(IGenServer server)
    {
        _servers.Add(server.Id, server);
    }

    public void Unregister(IGenServer server)
    {
        _servers.Remove(server.Id);
    }
}
