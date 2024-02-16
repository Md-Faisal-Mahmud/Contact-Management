using Contact_ManageMent.Domain.UnitOfWorks;

namespace Contact_Management.Application
{
    public interface IContactRepository : IUnitOfWork
    {
        IContactRepository Contacts { get; }
    }
}