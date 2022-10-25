namespace KS.Server.Interfaces;

public interface IOrganizationMessage<T> : IMessage
{
    public T Message { get; set; }
}
