namespace Contact_ManageMent.Domain.Entities
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
