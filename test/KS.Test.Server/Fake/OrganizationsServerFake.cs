using KS.Domain.Entities;
using KS.Server;
using KS.Server.Interfaces;
using KS.Server.Servers;

namespace KS.Test.Server.Fake;

internal class OrganizationsServerFake : GenServer<Organization>
{
    public OrganizationsServerFake(EventBus eventBus)
        : base(eventBus) { }

    public IMessage AssertedMessage { get; set; }


    public async Task ReceiveAsync(IMessage message)
    {
        await base.ReceiveAsync(message);
    }

    protected override void SetExecution(IMessage message)
    {
        AssertedMessage = message;
    }

    protected override void SetMessagesDictionary()
    {
        // 
    }
}
