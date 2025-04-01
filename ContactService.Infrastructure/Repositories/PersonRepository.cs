using ContactService.Core.Interfaces.Repository;
using ContactService.Core.Models;
using ContactService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Person> AddPersonAsync(Person person)
        {
             
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }
        public async Task<List<Person>> GetAllAsync()
        {
            //throw new Exception("Db Hata Test");
            return await _context.People.ToListAsync();
        }
        public async Task<Person?> GetByIdAsync(Guid personId)
        {
            return await _context.People.FindAsync(personId);
        }
        public async Task<Person?> GetDetailedByIdAsync(Guid personId)
        {
            return await _context.People
                .Include(p => p.ContactInformations)  
                .FirstOrDefaultAsync(p => p.Id == personId); 
        }


        public async Task DeleteAsync(Person person)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountPersonsByLocationAsync(string location)
        {
            return await _context.People
                .Where(p => p.ContactInformations.Any(ci => ci.InfoType == InfoTypeEnum.Location && ci.InfoContent == location))
                .CountAsync();
        }


        public async Task<int> CountPhoneNumbersByLocationAsync(string location)
        {
            return await _context.ContactInformations
         .Where(ci => ci.InfoType == InfoTypeEnum.Phone &&
                      ci.Person.ContactInformations.Any(c => c.InfoType == InfoTypeEnum.Location && c.InfoContent == location))
         .CountAsync();
        }
    }
}
