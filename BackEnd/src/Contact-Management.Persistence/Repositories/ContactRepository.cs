using Contact_Management.Application;
using Contact_Management.Application.Repositories;
using Contact_ManageMent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contact_Management.Persistence.Repositories
{
    public class ContactRepository : Repository<Contact, Guid>, IContactRepository
    {
        public ContactRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
    }
}
