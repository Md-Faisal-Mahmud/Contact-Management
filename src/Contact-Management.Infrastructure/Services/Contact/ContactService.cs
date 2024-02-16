using Contact_Management.Application;
using Contact_Management.Application.Services.Contact;

namespace Contact_Management.Infrastructure.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public ContactService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddContact(Contact_ManageMent.Domain.Entities.Contact contact)
        {
            Contact_ManageMent.Domain.Entities.Contact contact1 = contact;
            _unitOfWork.Contacts.Add(contact1);
            _unitOfWork.Save();
        }
    }
}