using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BirthdayManager.DataAccessLayer.DbContextFolder;

public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionBuilder.UseNpgsql(PostgreSqlConnectionString.Value, builder => 
            builder.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));

        return new ApplicationContext(optionBuilder.Options);
    }
}