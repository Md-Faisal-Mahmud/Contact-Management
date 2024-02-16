using Contact_ManageMent.Domain.Entities;
using Contact_ManageMent.Domain.Repositories;

namespace Contact_Management.Application.Repositories
{
    public interface IContactRepository : IRepositoryBase<Contact, Guid>
    {
    }
}