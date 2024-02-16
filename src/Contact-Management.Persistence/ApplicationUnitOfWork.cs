using Contact_Management.Application;
using Contact_Management.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contact_Management.Persistence
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IContactRepository Contacts { get; private set; }
    
        public ApplicationUnitOfWork(IApplicationDbContext dbContext,
            IContactRepository contactRepository) : base((DbContext)dbContext)
        {
            Contacts = contactRepository;
        }
    }
}