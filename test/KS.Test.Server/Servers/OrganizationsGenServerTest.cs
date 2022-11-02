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

    [Fact]
    public void NewOrganization()
    {
        var eventBus = new EventBus();
        var genServer = new OrganizationsServer(eventBus);
        var genServerFake = new OrganizationsServerFake(eventBus);
        eventBus.Register(genServer);
        eventBus.Register(genServerFake);

        var organizationId = new Guid("4027335a-39d4-4226-a8a1-ddc6248c7aab");
        var organization = new Organization(organizationId);

        var message = new CreateOrganization(organization, genServerFake.Id);

        eventBus.Communicate(genServer.Id, message).Wait();

        genServer.ReceiveAsync(message);
        Thread.Sleep(3000);

        var retrieved = new CreatedOrganization()
        {
            From = genServer.Id,
            Organization = organization
        };

        Assert.Equal(genServerFake.AssertedMessage.From, retrieved.From);
        Assert.Equal(((CreatedOrganization)genServerFake.AssertedMessage).Organization.Id, retrieved.Organization.Id);
    }

    [Fact]
      public void ExistentOrganization()
    {
        var eventBus = new EventBus();
        var genServer = new OrganizationsServer(eventBus);
        var genServerFake = new OrganizationsServerFake(eventBus);
        eventBus.Register(genServer);
        eventBus.Register(genServerFake);

        var organizationId = new Guid("4027335a-39d4-4226-a8a1-ddc6248c7aab");
        var organization = new Organization(organizationId);

        var message = new CreateOrganization(organization, genServerFake.Id);

        eventBus.Communicate(genServer.Id, message).Wait();
        eventBus.Communicate(genServer.Id, message).Wait();

        genServer.ReceiveAsync(message);
        Thread.Sleep(3000);
        genServer.ReceiveAsync(message);
        Thread.Sleep(3000);

        var retrieved = new ExistsOrganization()
        {
            From = genServer.Id,
            Error = new Exception("Organization already exists")
        };

        Assert.Equal(genServerFake.AssertedMessage.From, retrieved.From);
        Assert.Equal(((ExistsOrganization)genServerFake.AssertedMessage).Error.Message, retrieved.Error.Message);
    }
}