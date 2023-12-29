using ISI.Domain.Entity;
using ISI.infrastructure;
using ISI.infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ISI.Infrastructure
{
    public class IsiDbContextFactory : IDesignTimeDbContextFactory<IsiDbContext>
    {
        public IsiDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<IsiDbContext>();
            var connectionString = configuration.GetConnectionString("IsiDatabase");

            builder.UseNpgsql(connectionString);

            return new IsiDbContext(builder.Options);
        }
    }
}