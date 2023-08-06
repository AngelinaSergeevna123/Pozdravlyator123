using BirthdayManager.BusinessLogic.Models;
using BirthdayManager.DataAccessLayer.Repositories;
using Castle.DynamicProxy.Generators;

namespace BirthdayManager.BusinessLogic.Services;

public class BirthdayService : IBirthdayService
{
    private readonly IUserRepository _userRepository;

    public BirthdayService(IUserRepository userRepository) => _userRepository = userRepository;

    public IEnumerable<Person> GetPeople(PagginationOptions options)
    {
        var dbPersons = _userRepository.GetPersons(new(options.Offset, options.Limit));
        var persons = dbPersons.Select(dbPerson => new Person(dbPerson.Name, dbPerson.Gender,
            CalculateAge(dbPerson.Birthdate.Year, DateTime.Now.Year), dbPerson.Birthdate));

        return persons;
    }
    
    public DateTime GetClosestBirthday()
    {
        var currentYear = DateTime.Now.Year;
        var closestBirthday = DateTime.MaxValue;

        var people = _userRepository.GetPersons(
            new(0, _userRepository.GetCountOfUsers()));
        
        foreach (var person in people)
        {
            var offset = 1;
            var nextBirthday = new DateTime(currentYear, person.Birthdate.Month, person.Birthdate.Day);
            if (nextBirthday < DateTime.Now) nextBirthday = nextBirthday.AddYears(offset);

            if (nextBirthday < closestBirthday) closestBirthday = nextBirthday;
        }

        return closestBirthday;
    }

    public async Task<Person> FindPersonByName(string name)
    {
        var user = await _userRepository.FindUserByName(name);
        return new Person(user.Name, user.Gender, CalculateAge(user.Birthdate.Year,
            DateTime.Now.Year), user.Birthdate);
    }

    public async Task<bool> RemovePerson(string name)
    {
        var personToRemove = FindPersonByName(name);

        if (personToRemove is null) return false;

        await _userRepository.RemoveUserByName(name);
        
        return true;
    }

    public async Task AddPerson(Person person) =>
        await _userRepository.AddPerson(new(person.Gender, person.Name, person.Birthdate));
    
    private int CalculateAge(int birthdayYear, int currentYear) => currentYear - birthdayYear;
}