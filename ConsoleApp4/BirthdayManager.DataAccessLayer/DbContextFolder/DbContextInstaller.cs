using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BirthdayManager.DataAccessLayer.DbContextFolder;

public static class DbContextInstaller
{
    public static void ConfigureDbContext(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>(o => o.UseLazyLoadingProxies()
            .UseNpgsql(PostgreSqlConnectionString.Value, builder =>
                builder.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
    }
}