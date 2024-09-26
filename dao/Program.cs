using dao.Persistence.Mapping;
using dao.DTOs;
using dao.Persistence.DAO;
using dao.Persistence.Utils;
public class Program
{
    public static void Main()
    {
        // Crear una instància del DAO
        IContactDAO contactDAO = new ContactDAO(NpgsqlUtils.OpenConnection());

        // Exemple d'ús del DAO
        ContactDTO newContact = new ContactDTO
        {
            Name = "Miquel",
            Surname = "Font"
        };

        // Afegir un nou contacte
        try {
            contactDAO.AddContact(newContact);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        //Recuperar el contacte per id
        try {
            var contact = contactDAO.GetContactById(3);
            Console.WriteLine($"ID: {contact.Id}, Nom: {contact.Name} {contact.Surname}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }   

        //Recuperar tots els contactes
        var contacts = contactDAO.GetAllContacts();
        Console.WriteLine($"ID | Nom  \t| Cognom");
        foreach (var row in contacts)
        {
            Console.WriteLine($"{row.Id}  | {row.Name}  \t| {row.Surname}");
        }
        
        // Actualitzar un contacte   
        try {
            ContactDTO contact = new ContactDTO
            {
                Name = "Joan",
                Surname = "Saura"
            };
            contactDAO.UpdateContact(contact);
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        // Eliminar un contacte
        
        try { 
            contactDAO.DeleteContact(8);
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
