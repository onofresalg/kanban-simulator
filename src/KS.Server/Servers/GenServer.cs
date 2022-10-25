using KS.Domain.Entities;
using KS.Server.Interfaces;

namespace KS.Server.Servers;

public abstract class GenServer<T> : IGenServer where T : GenEntity
{
    public Guid Id { get; }
    protected Queue<IMessage> _messages;
    protected Dictionary<Type, int> _messageTypes;
    protected Dictionary<Guid, T> _states;
    protected EventBus _eventBus;

    public GenServer(EventBus eventBus)
    {
        _eventBus = eventBus;
        //Id = Guid.NewGuid();
        SetMessagesDictionary();
        Start().Wait();
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
        while (true)
        {
            if (_messages.Count > 0)
            {
                SetExecution(_messages.Dequeue());
            }
            Thread.Sleep(1000);
        }
    }

    protected abstract void SetExecution(IMessage message);

    protected abstract void SetMessagesDictionary();
}
