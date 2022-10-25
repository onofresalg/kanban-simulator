namespace KS.Server.Interfaces;

public interface IGenServer
{
    Guid Id { get; }

    Task ReceiveAsync(IMessage message);
}
