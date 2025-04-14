using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thread.Infrastructure.Configurations;
using Thread.Infrastructure.Exstensions;
using Thread.Persistence.Options;

namespace Thread.Persistence;

/// <summary>
/// Represents the persistence service installer.
/// </summary>
internal sealed class PersistenceServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddMemoryCache()
            .ConfigureOptions<ConnectionStringSetup>()
            .AddTransientAsMatchingInterfaces(AssemblyReference.Assembly);
}
