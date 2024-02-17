using Contact_ManageMent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contact_Management.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Contact> Contacts { get; set; }
    }
}
