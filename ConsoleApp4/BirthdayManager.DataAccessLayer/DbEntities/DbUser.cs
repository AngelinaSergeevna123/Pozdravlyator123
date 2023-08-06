using System.ComponentModel.DataAnnotations.Schema;

namespace BirthdayManager.DataAccessLayer.DbEntities;

public class DbUser
{
    [ForeignKey($"{nameof(Id)}")]
    public int Id { get; set; }
    public string Gender { get; set; }
    public string Name { get; set; }
    public DateTime Birthdate { get; set; }
    public bool IsDeleted { get; set; }

    public DbUser(string gender, string name, DateTime birthdate) => (Gender, Name, Birthdate)  = (gender, name, birthdate);
}