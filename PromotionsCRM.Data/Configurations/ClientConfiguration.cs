using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromotionsCRM.Data.Models;

namespace PromotionsCRM.Web.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasData(
                new Client { Id = 1, Name = "Canon", Email = "canon@example.com", Address = "123 Canon Street", CountryId = 1, PhoneNumber = "123-456-7890", RegisteredOn = new DateTime(2026, 1, 15), Website = "www.canon.com" },
                   new Client { Id = 2, Name = "Nikon", Email = "nikon@example.com", Address = "456 Nikon Avenue", CountryId = 1, PhoneNumber = "987-654-3210", RegisteredOn = new DateTime(2026, 1, 15), Website = "www.nikon.com" },
                   new Client { Id = 3, Name = "Sony", Email = "sony@example.com", Address = "789 Sony Boulevard", CountryId = 1, PhoneNumber = "555-555-5555", RegisteredOn = new DateTime(2026, 1, 15), Website = "www.sony.com" }
            );
        }
    }
}
