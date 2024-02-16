using Contact_Management.Application.Repositories;
using Contact_ManageMent.Domain.UnitOfWorks;

namespace Contact_Management.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IContactRepository Contacts { get; }
    }
}