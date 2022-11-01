using KS.Domain.Entities;
using KS.Server.Interfaces;
using KS.Server.Messages;

namespace KS.Server.Servers;

public class OrganizationServer : GenServer<Organization>
{
    private Organization _states;

    public OrganizationServer(EventBus eventBus)
        : this(new Organization(), eventBus) { }

    public OrganizationServer(Organization states, EventBus eventBus)
        : base(eventBus)
    {
        _states = states;
    }

    protected override void SetExecution(IMessage message)
    {
        switch (_messageTypes[message.GetType()])
        {
            case 0:
                Execute((ChangeOrganizationName)message);
                break;
            default:
                break;
        }
    }

    protected override void SetMessagesDictionary()
    {
        _messageTypes = new Dictionary<Type, int>
        {
            { typeof(ChangeOrganizationName), 0 },
            //{ typeof(DeleteOrganization), 1 },
            //{ typeof(RetrieveAllOrganizations), 2 }
        };
    }

    private async void Execute(ChangeOrganizationName message)
    {
        _states.Name = message.Message.Name;
        var organization = (Organization)_states.Clone();
        //_eventBus.Communicate(message.From, )

        //TODO: Devolver a resposta de sucesso ou erro em caso de atributos iguais ou mesmo GUID
    }
}
