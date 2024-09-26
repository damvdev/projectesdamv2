using Npgsql;
using dao.DTOs;
using dao.Persistence.DAO;
using dao.Persistence.Utils;

namespace dao.Persistence.Mapping
{
    public class ContactDAO : IContactDAO
    {
        private readonly string connectionString;

        public ContactDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ContactDTO GetContactById(int id)
        {
            ContactDTO contact = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(NpgsqlUtils.OpenConnection()))
            {
                string query = "SELECT \"ID\", \"FirstName\", \"LastName\" FROM \"Contact\" WHERE \"ID\" = @Id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // ORM: [--,--,--] -----> ContactDTO
                    contact = NpgsqlUtils.GetContact(reader);
                }
            }

            return contact;
        }

        public void AddContact(ContactDTO contact)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "INSERT INTO \"Contact\" (\"FirstName\", \"LastName\") VALUES (@FirstName, @LastName)";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", contact.Name);
                command.Parameters.AddWithValue("@LastName", contact.Surname);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdateContact(ContactDTO contact)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "UPDATE \"Contact\" SET \"FirstName\" = @Name, \"LastName\" = @Surname WHERE \"ID\" = @Id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", contact.Name);
                command.Parameters.AddWithValue("@Surname", contact.Surname);
                command.Parameters.AddWithValue("@Id", contact.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteContact(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "DELETE FROM \"Contact\" WHERE \"ID\" = @Id";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<ContactDTO> GetAllContacts()
        {
            List<ContactDTO> contacts = new List<ContactDTO>();

            using (NpgsqlConnection connection = new NpgsqlConnection(NpgsqlUtils.OpenConnection()))
            {
                string query = "SELECT \"ID\", \"FirstName\", \"LastName\" FROM \"Contact\"";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // ORM: [--,--,--] -----> ContactDTO                  
                    ContactDTO contact = NpgsqlUtils.GetContact(reader);
                    contacts.Add(contact);
                }
            }
            return contacts;
        }
    }

}
