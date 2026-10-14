using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromotionsCRM.Data.Models;

namespace PromotionsCRM.Web.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
                new Customer { Id = 1, FirstName = "Ivan",   LastName = "Petrov",  Email = "ivan.petrov@example.com",   PhoneNumber = "359-888-1234", CountryId = 1,  RegisteredOn = new DateTime(2026, 2, 10) },
                new Customer { Id = 2, FirstName = "Maria",  LastName = "Georgieva", Email = "maria.g@example.com",     PhoneNumber = "359-877-5678", CountryId = 1,  RegisteredOn = new DateTime(2026, 3, 5) },
                new Customer { Id = 3, FirstName = "James",  LastName = "Smith",   Email = "james.smith@example.com",   PhoneNumber = "447-700-9001", CountryId = 3,  RegisteredOn = new DateTime(2026, 4, 18) },
                new Customer { Id = 4, FirstName = "Anna",   LastName = "Muller",  Email = "anna.muller@example.com",   PhoneNumber = "491-512-3456", CountryId = 4,  RegisteredOn = new DateTime(2026, 1, 22) },
                new Customer { Id = 5, FirstName = "Kenji",  LastName = "Tanaka",  Email = "kenji.tanaka@example.com",  PhoneNumber = "819-012-3456", CountryId = 20, RegisteredOn = new DateTime(2026, 5, 30) }
            );
        }
    }
}
