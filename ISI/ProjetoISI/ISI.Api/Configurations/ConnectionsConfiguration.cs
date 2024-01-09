using ISI.infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ISI.Api.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConections(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbConnection(configuration);
        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration
            .GetConnectionString("IsiDatabase");
        services.AddDbContext<IsiDbContext>(
            options => options.UseNpgsql(
                connectionString)
        );
        return services;
    }
}
