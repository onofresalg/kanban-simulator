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

    private Task Execute(CreateOrganization message)
    {
        IMessage result;
        if (FindByName(message.Message.Name) == null) {
            _states.Add(message.Message.Id, message.Message);
            var server = new OrganizationServer(message.Message, _eventBus);
            _eventBus.Register(server);
            result = new CreatedOrganization{Organization = message.Message, From = Id};
        } else {
            result = new ExistsOrganization{Error = new Exception("Organization already exists"), From = Id};
        }

        return _eventBus.Communicate(message.From, result);
    }

    private void Execute(DeleteOrganization message)
    {
        _states.Remove(message.Message);
        //TODO: Devolver a resposta de sucesso/erro para o from
    }

    private Task Execute(RetrieveAllOrganizations message)
    {
        var newMessage = new RetrievedOrganizations()
        {
            From = Id,
            Organizations = _states.Select(org => org.Value).ToList()
        };
        return _eventBus.Communicate(message.From, newMessage);
        //TODO: Devolver a resposta de sucesso/erro para o from
    }

    private Organization? FindByName(string name)
    {
        return _states.Select(state => state.Value).Where(state => state.Name == name).SingleOrDefault();
    }
}
