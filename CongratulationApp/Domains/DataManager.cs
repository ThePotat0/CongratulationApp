using CongratulationApp.Domains.Repositories.Abstract;

namespace CongratulationApp.Domains
{
    public class DataManager
    {
        public IContactsRepository Contacts { get; set; }

        public DataManager(IContactsRepository contactsRepository) 
        {
            Contacts = contactsRepository;
        }
    }
}
