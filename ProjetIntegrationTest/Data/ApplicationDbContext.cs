using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetIntegrationTest.Models;

namespace ProjetIntegrationTest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            CreateUsersAndRoles(builder);
        }

        private static void CreateUsersAndRoles(ModelBuilder builder)
        {
            var adminId = "8e445865-a24d-4543-a6c6-9443d048cdb9";

            var CustomerRoleId = "1d8ac862-e54d-4f10-b6f8-638808c02967";
            var adminRoleId = "4ce0a981-6965-4a1e-9b36-f330ea2651f1";

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminId,
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                    ConcurrencyStamp = "403698ff-c91e-459e-aa9f-44bb1477977d",
                    SecurityStamp = "37JEAHACN3NJOTTOFZ4HRY5XRFHNA3OF",
                    PasswordHash = "AQAAAAIAAYagAAAAEKUmW9w2Gm1D6K6Wg9BELuoORIGR46DLWLOb+L07IsPD2BGU4A2bBIih+eGQzyuVhw==",
                }
            );

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = adminRoleId,
                    Name = ApplicationRoles.Administrator,
                    NormalizedName = ApplicationRoles.Administrator.ToUpper(),
                    ConcurrencyStamp = "0609dc4a-a0c7-422b-98f1-a7f2fb6a23ce"
                },
                new IdentityRole()
                {
                    Id = CustomerRoleId,
                    Name = ApplicationRoles.Customer,
                    NormalizedName = ApplicationRoles.Customer.ToUpper(),
                    ConcurrencyStamp = "0609dc4a-a0c7-422b-98f1-a7f2fb6a23ce"
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminId
                }
            );
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bets { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
