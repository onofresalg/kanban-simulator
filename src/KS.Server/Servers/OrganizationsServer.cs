using KS.Domain.Entities;
using KS.Server.Interfaces;
using KS.Server.Messages;

namespace KS.Server.Servers;

public class OrganizationsServer : GenServer<Organization>
{
    private Dictionary<Guid, Organization> _states;

    public OrganizationsServer(EventBus eventBus)
        : this(new Dictionary<Guid, Organization>(), eventBus) { }

    public OrganizationsServer(Dictionary<Guid, Organization> states, EventBus eventBus)
        : base(eventBus)
    {
        _states = states;
    }

    protected override void SetMessagesDictionary()
    {
        _messageTypes = new Dictionary<Type, int>
        {
            { typeof(CreateOrganization), 0 },
            { typeof(DeleteOrganization), 1 },
            { typeof(RetrieveAllOrganizations), 2 }
        };
    }

    public override Task ReceiveAsync(IMessage message)
    {
        return base.ReceiveAsync(message);
    }

    protected override void SetExecution(IMessage message)
    {
        switch (_messageTypes[message.GetType()])
        {
            case 0:
                Execute((CreateOrganization)message);
                break;
            case 1:
                Execute((DeleteOrganization)message);
                break;
            case 2:
                Execute((RetrieveAllOrganizations)message);
                break;
            default:
                break;
        }
    }

    private async void Execute(CreateOrganization message)
    {
        _states.Add(message.Message.Id, message.Message);
        //TODO: Devolver a resposta de sucesso ou erro em caso de atributos iguais ou mesmo GUID
    }

    private async void Execute(DeleteOrganization message)
    {
        _states.Remove(message.Message);
        //TODO: Devolver a resposta de sucesso/erro para o from
    }

    private async void Execute(RetrieveAllOrganizations message)
    {
        var newMessage = new RetrievedOrganizations()
        {
            From = Id,
            Organizations = _states.Select(org => org.Value).ToList()
        };
        _eventBus.Communicate(message.From, newMessage);
        //TODO: Devolver a resposta de sucesso/erro para o from
    }
}
