using KS.Domain.Entities;
using KS.Server;
using KS.Server.Messages;
using KS.Server.Servers;
using KS.Test.Server.Fake;

namespace KS.Test.Server.Servers;

public class OrganizationsGenServerTest
{
    [Fact]
    public void EmptyServer()
    {
        var eventBus = new EventBus();
        var genServer = new OrganizationsServer(eventBus);
        var genServerFake = new OrganizationsServerFake(eventBus);
        eventBus.Register(genServer);
        eventBus.Register(genServerFake);

        var message = new RetrieveAllOrganizations()
        {
            From = genServerFake.Id
        };

        eventBus.Communicate(genServer.Id, message).Wait();

        genServer.ReceiveAsync(message);
        Thread.Sleep(3000);

        var retrieved = new RetrievedOrganizations
        {
            From = genServer.Id,
            Organizations = new List<Organization>()
        };

        Assert.Equal(genServerFake.AssertedMessage.From, retrieved.From);
        Assert.Equal(((RetrievedOrganizations)genServerFake.AssertedMessage).Organizations, retrieved.Organizations);
    }
}