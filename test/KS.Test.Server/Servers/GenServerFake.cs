using KS.Server.Interfaces;

namespace KS.Test.Server.Servers;

internal class GenServerFake : IGenServer
{
    public Guid Id { get; }

    public Task ReceiveAsync(IMessage message)
    {
        throw new NotImplementedException();
    }
}
