using Autofac;
using Contact_Management.Application.Services.Contact;
using Contact_ManageMent.Domain.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Contact_MangeMent.API.Models.Contact
{
    public class ContactCreateModel
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        private IContactService _contactService;
        public ContactCreateModel()
        {
        }

        public ContactCreateModel(IContactService contactService)
        {
            _contactService = contactService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _contactService = scope.Resolve<IContactService>();
        }

        public void Add(Guid currentUserID)
        {
            Contact_ManageMent.Domain.Entities.Contact contact = new Contact_ManageMent.Domain.Entities.Contact
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                Address = Address,
                UserId = currentUserID
            };

            _contactService.AddContact(contact);   
        }
    }
}
