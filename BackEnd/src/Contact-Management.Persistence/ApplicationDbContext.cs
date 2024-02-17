using Contact_Management.Persistence.Membership;
using Contact_ManageMent.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Contact_Management.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
         ApplicationRole, Guid,
         ApplicationUserClaim, ApplicationUserRole,
         ApplicationUserLogin, ApplicationRoleClaim,
         ApplicationUserToken>,
         IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    x => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Contact> Contacts { get; set; }

    }
}
