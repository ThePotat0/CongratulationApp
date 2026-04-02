using CongratulationApp.Domains.Entities;
using CongratulationApp.Domains.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CongratulationApp.Domains.Repositories.EntityFranework
{
    public class EFContactsRepository : IContactsRepository
    {
        private readonly AppDbContext _context;
        public EFContactsRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactEntity>> GetContactEntitiesAsync() 
        {
            return await _context.AllСontactEntities.Include(x => x.Contacts).ToListAsync();
        }

        public async Task<ContactEntity?> GetContactByIdAsync(int id) 
        {
            return await _context.AllСontactEntities.Include(x => x.Contacts).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task SaveContactAsync(ContactEntity entity) 
        {
            _context.Entry(entity).State = entity.Id == default ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id) 
        {
            _context.Entry(new ContactEntity() { Id = id }).State = EntityState.Deleted;
            await  _context.SaveChangesAsync();
        }
    }
}
