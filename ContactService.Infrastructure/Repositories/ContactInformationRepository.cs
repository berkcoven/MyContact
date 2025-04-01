using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactService.Core.Interfaces.Repository;
using ContactService.Core.Models;
using ContactService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Repositories
{
    public class ContactInformationRepository : IContactInformationRepository
    {
        private readonly AppDbContext _context;

        public ContactInformationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ContactInformation> AddAsync(ContactInformation entity)
        {
            _context.ContactInformations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var contactInfo = await _context.ContactInformations.FindAsync(id);
            if (contactInfo != null)
            {
                _context.ContactInformations.Remove(contactInfo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ContactInformation> GetByIdAsync(Guid id)
        {
            return await _context.ContactInformations
                .FindAsync(id);
        }
    }
}
