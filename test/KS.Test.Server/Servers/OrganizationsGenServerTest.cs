using KS.Server;
using KS.Server.Interfaces;
using KS.Server.Messages;
using KS.Server.Servers;
using Moq;

namespace KS.Test.Server.Servers;

public class OrganizationsGenServerTest
{
    [Fact]
    public void EmptyServer()
    {
        var eventBus = new EventBus();
        var genServer = new OrganizationsServer(eventBus);
        eventBus.Register(genServer);

        var genServerMock = new Mock<IGenServer>();
        genServerMock.Setup(mock => mock.ReceiveAsync(It.IsAny<RetrieveAllOrganizations>()));

        var message = new RetrieveAllOrganizations()
        {
            From = Guid.NewGuid()
        };
        genServer.ReceiveAsync();
    }
}