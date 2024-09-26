using Microsoft.Extensions.Configuration;
using Npgsql;
using dao.DTOs;

namespace dao.Persistence.Utils
{
    public class NpgsqlUtils
    {
        public static string OpenConnection() {
            // Carregar la cadena de connexió a la base de dades des de l'arxiu de configuració
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(@"\dao\dao\appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return config.GetConnectionString("MyPostgresConn");
        }

        public static ContactDTO GetContact(NpgsqlDataReader reader) 
        {
            ContactDTO c = new ContactDTO
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Surname = reader.GetString(2)
            };
            return c;
        }

    }
}
