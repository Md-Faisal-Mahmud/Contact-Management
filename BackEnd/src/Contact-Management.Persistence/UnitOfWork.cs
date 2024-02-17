using Contact_ManageMent.Domain.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Contact_Management.Persistence
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext) => _dbContext = dbContext;

        public virtual void Dispose() => _dbContext?.Dispose();
        public virtual void Save() => _dbContext?.SaveChanges();
    }
}