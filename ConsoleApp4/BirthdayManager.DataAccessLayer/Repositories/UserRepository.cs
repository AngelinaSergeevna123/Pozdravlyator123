using BirthdayManager.DataAccessLayer.DbContextFolder;
using BirthdayManager.DataAccessLayer.DbEntities;
using BirthdayManager.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BirthdayManager.DataAccessLayer.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _db;

    public UserRepository(ApplicationContext db) => _db = db;

    public async Task AddPerson(DbUser dbUser)
    {
        await _db.AddAsync(dbUser);
        await _db.SaveChangesAsync();
    }

    public IEnumerable<DbUser> GetPersons(PagginationOptions options) 
        => _db.Users.Where(x => !x.IsDeleted).Skip(options.Offset).Take(options.Limit).ToArray();
    
    public async Task RemoveUserByName(string name)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => string.Equals(x.Name, name));
            user!.IsDeleted = true;

        await _db.SaveChangesAsync();
    }
    
    public int GetCountOfUsers() => _db.Users.Count();

    public async Task<DbUser?> FindUserByName(string name) 
        => await _db.Users.FirstOrDefaultAsync(x => string.Equals(x.Name, name));
}