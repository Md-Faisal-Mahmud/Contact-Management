namespace Contact_Management.Application.Services.Contact
{
    public interface IContactService
    {
        void AddContact(Contact_ManageMent.Domain.Entities.Contact contact);
        void UpdateContact(Guid Id, Contact_ManageMent.Domain.Entities.Contact contact);
    }
}