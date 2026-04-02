using CongratulationApp.Domains.Entities;

namespace CongratulationApp.Domains.Repositories.Abstract
{
    public interface IContactsRepository
    {
        Task<IEnumerable<ContactEntity>> GetContactEntitiesAsync();
        Task<ContactEntity?> GetContactByIdAsync(int id);
        Task SaveContactAsync(ContactEntity entity);
        Task DeleteContactAsync(int id);
    }
}
