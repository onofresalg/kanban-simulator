using KS.Domain.Entities;
using KS.Server.Interfaces;

namespace KS.Server.Servers;

public abstract class GenServer<Entity> : IGenServer where Entity : GenEntity
{
    public Guid Id { get; protected set; }
    protected Queue<IMessage> _messages;
    protected Dictionary<Type, int> _messageTypes;
    protected EventBus _eventBus;

    public GenServer(EventBus eventBus)
    {
        _eventBus = eventBus;
        _messages = new Queue<IMessage>();
        Id = Guid.NewGuid();
        SetMessagesDictionary();
        Start();
    }

    public virtual async Task ReceiveAsync(IMessage message)
    {
        await Task.Run(() =>
        {
            _messages.Enqueue(message);
        });
    }

    private Task Start()
    {
        //TODO: Verificar Tail Recursion
        //TODO: Otimizar o thread sleep
        return Task.Factory.StartNew(() =>
        {
            while (true)
            {
                if (_messages.Count > 0)
                {
                    SetExecution(_messages.Dequeue());
                }
                Thread.Sleep(1000);
            }
        });
    }

    protected abstract void SetExecution(IMessage message);

    protected abstract void SetMessagesDictionary();
}
