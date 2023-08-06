using BirthdayManager.DataAccessLayer.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace BirthdayManager.DataAccessLayer.DbContextFolder;

public class ApplicationContext : DbContext
{
    public DbSet<DbUser> Users { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
}