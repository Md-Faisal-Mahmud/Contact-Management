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

        public void UpdateContact(Guid Id, Contact_ManageMent.Domain.Entities.Contact contact)
        {
            var _contact = _unitOfWork.Contacts.GetById(Id);
            if (_contact.UserId == contact.UserId)
            {
                _contact.Phone = contact.Phone;
                _contact.Address = contact.Address;
                _contact.Email = contact.Email;
                _contact.Name = contact.Name;
                _unitOfWork.Save();
            }
            else {
                throw new ForbiddenException("You try to edit someone else contact!");
            }
        }
    }
}