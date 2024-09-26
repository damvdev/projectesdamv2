using dao.DTOs;

namespace dao.Persistence.DAO
{
    public interface IContactDAO
    {
        ContactDTO GetContactById(int id);
        public IEnumerable<ContactDTO> GetAllContacts();
        void AddContact(ContactDTO contact);
        void UpdateContact(ContactDTO contact);
        void DeleteContact(int id);
    }
}
