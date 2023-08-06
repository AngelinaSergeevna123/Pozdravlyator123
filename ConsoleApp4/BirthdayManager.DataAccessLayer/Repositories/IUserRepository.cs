using BirthdayManager.DataAccessLayer.DbEntities;
using BirthdayManager.DataAccessLayer.Models;

namespace BirthdayManager.DataAccessLayer.Repositories;

public interface IUserRepository
{
    Task AddPerson(DbUser dbUser);
    IEnumerable<DbUser> GetPersons(PagginationOptions options);
    Task RemoveUserByName(string name);
    int GetCountOfUsers();
    Task<DbUser?> FindUserByName(string name);
}