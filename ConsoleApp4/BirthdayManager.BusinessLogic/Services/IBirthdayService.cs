using BirthdayManager.BusinessLogic.Models;

namespace BirthdayManager.BusinessLogic.Services;

public interface IBirthdayService
{
    DateTime GetClosestBirthday();
    Task<Person> FindPersonByName(string name);
    Task<bool> RemovePerson(string name);
    Task AddPerson(Person person);
    IEnumerable<Person> GetPeople(PagginationOptions options);
}