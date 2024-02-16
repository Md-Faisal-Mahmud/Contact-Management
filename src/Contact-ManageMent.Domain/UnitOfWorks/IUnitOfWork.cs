namespace Contact_ManageMent.Domain.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}