using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KS.Presentation;

public class Presentation
{
    private IHost _host { get; }

    public Presentation(string[] args, Action<IServiceCollection> services)
    {
        var builder = Host.CreateDefaultBuilder(args);
        builder.ConfigureServices(services);
        _host = builder.Build();
    }

    public Task RunAsync()
    {
        return _host.RunAsync();
    }
}
