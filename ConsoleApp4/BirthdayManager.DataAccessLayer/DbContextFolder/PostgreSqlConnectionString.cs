using Npgsql;

namespace BirthdayManager.DataAccessLayer.DbContextFolder;

public class PostgreSqlConnectionString
{
    public static string Value
    {
        get
        {
            var npgsqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder();
                npgsqlConnectionStringBuilder.Host = "localhost";
                npgsqlConnectionStringBuilder.Port = 5432;
                npgsqlConnectionStringBuilder.Database = "BirthdayManager";
                npgsqlConnectionStringBuilder.Username = "postgres";
                npgsqlConnectionStringBuilder.Password = "ultraMegaSuperDruperSlochniyParolTetriandohAneTetridanoh12343";

            return npgsqlConnectionStringBuilder.ConnectionString;
        }
    }
}