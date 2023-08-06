using BirthdayManager.BusinessLogic.Services;
using BirthdayManager.DataAccessLayer.DbContextFolder;
using BirthdayManager.DataAccessLayer.Repositories;
using ConsoleApp4;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
    services.ConfigureDbContext();
    services.AddEntityFrameworkNpgsql();
    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<IBirthdayService, BirthdayService>();
    services.AddTransient<ConsoleLogic>();

await using var serviceProvider = services.BuildServiceProvider();
var consoleLogic = serviceProvider.GetService<ConsoleLogic>();
await consoleLogic!.Proceed();



