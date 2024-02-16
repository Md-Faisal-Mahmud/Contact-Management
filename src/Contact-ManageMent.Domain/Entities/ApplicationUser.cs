using Microsoft.AspNetCore.Identity;

namespace Contact_ManageMent.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IEntity<Guid>
    {
        public override Guid Id { get; set; }
        public ICollection<Contact>? Contacts { get; set; }
    }
}