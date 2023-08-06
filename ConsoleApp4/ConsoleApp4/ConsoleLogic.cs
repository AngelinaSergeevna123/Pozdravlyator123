using System.Globalization;
using BirthdayManager.BusinessLogic;
using BirthdayManager.BusinessLogic.Services;

namespace ConsoleApp4;

public class ConsoleLogic
{
    private readonly IBirthdayService _birthdayService;

    public ConsoleLogic(IBirthdayService birthdayService) => _birthdayService = birthdayService;

    public async Task Proceed()
    {
        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить человека");
            Console.WriteLine("2. Удалить человека из списка");
            Console.WriteLine("3. Показать список людей");
            Console.WriteLine("4. Вывести напоминание о ближайшей дате рождения");
            Console.WriteLine("5. Выйти");
            Console.Write("Введите номер действия: ");

            var choice = int.Parse(Console.ReadLine()!);

            switch (choice)
            {
                case 1:
                    await AddPerson();
                    break;
                case 2:
                    await RemovePerson();
                    break;
                case 3:
                    ShowPeople();
                    break;
                case 4:
                    GetClosestBirthday();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный номер действия!");
                    break;
            }
        }
    }

        async Task AddPerson()
        {
            Console.Write("Введите имя: ");
            var name = Console.ReadLine();

            Console.Write("Введите пол: ");
            var gender = Console.ReadLine();

            Console.Write("Введите возраст: ");
            var age = int.Parse(Console.ReadLine());

            Console.Write("Введите дату рождения (дд/мм/гггг): ");
            var birthdate = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", null).ToUniversalTime();
                birthdate = birthdate.AddHours(5);
    
            await _birthdayService.AddPerson(new Person(name!, gender!, age, birthdate));

            Console.WriteLine("Человек успешно добавлен!");
        }

        async Task RemovePerson()
        {
            Console.Write("Введите имя человека для удаления из списка: ");
    
            var name = Console.ReadLine();

            var text = await _birthdayService.RemovePerson(name!) ? "Человек успешно удален из списка!" : "Человек не найден!";
    
            Console.WriteLine(text);
        }


        void ShowPeople()
        {
            Console.WriteLine("Укажите, сколько человек на одной странице вы хотите видеть: ");
            var limit = int.Parse(Console.ReadLine()!);
            var page = 0;
            while (true)
            {
                var offset = page++ * limit;
                var people = _birthdayService.GetPeople(new(offset, limit)).ToList();
    
                Console.WriteLine("Ниже будет список людей. Желаете ли его отсортировать по дате рождения?" +
                                  "Нажмите 1, если да. Если нет, то цифра 2");

                if (int.Parse(Console.ReadLine()!) is 1) 
                    people.Sort((x, y) => x.Birthdate.CompareTo(y.Birthdate));

                Console.WriteLine("Список людей:");
    
                foreach (var person in people)
                    Console.WriteLine(
                        $"Имя: {person.Name}, Пол: {person.Gender}, Возраст: {person.Age}, Дата рождения: {person.Birthdate:dd/MM/yyyy}");
                
                Console.WriteLine("Введите цифру 1, чтобы продолжить листать или цифру 2, чтобы выйти в меню");

                if (int.Parse(Console.ReadLine()!) is not 1) return;
            }
        }

        void GetClosestBirthday() 
            => Console.WriteLine($"Ближайшая дата рождения: {_birthdayService.GetClosestBirthday():dd/MM/yyyy}");
}