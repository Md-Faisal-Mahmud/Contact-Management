using Autofac;
using Contact_Management.Application.Services.Contact;

namespace Contact_MangeMent.API.Models.Contact
{
    public class ContactListModel
    {
        private IContactService _contactService;
        public ContactListModel()
        {
            
        }

        public ContactListModel(IContactService contactService)
        {
            _contactService = contactService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _contactService = scope.Resolve<IContactService>();
        }

        public void Delete(Guid ContactId,Guid CurrentUserId) 
        {
           _contactService.DeleteContact(ContactId, CurrentUserId);
        }

        public IList<Contact_ManageMent.Domain.Entities.Contact> GetContacts(Guid CurrentUserId)
        {
            return _contactService.GetContacts(CurrentUserId);
        }
    }
}
