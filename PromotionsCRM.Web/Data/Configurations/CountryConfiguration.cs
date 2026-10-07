using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromotionsCRM.Data.Models;

namespace PromotionsCRM.Web.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
           builder.HasData(
                new Country { Id = 1, Name = "Bulgaria" },
                new Country { Id = 2, Name = "United States" },
                new Country { Id = 3, Name = "United Kingdom" },
                new Country { Id = 4, Name = "Germany" },
                new Country { Id = 5, Name = "France" },
                new Country { Id = 6, Name = "Italy" },
                new Country { Id = 7, Name = "Spain" },
                new Country { Id = 8, Name = "Canada" },
                new Country { Id = 9, Name = "Australia" },
                new Country { Id = 10, Name = "Austria" },
                new Country { Id = 11, Name = "Belgium" },
                new Country { Id = 12, Name = "Croatia" },
                new Country { Id = 13, Name = "Czech Republic" },
                new Country { Id = 14, Name = "Denmark" },
                new Country { Id = 15, Name = "Estonia" },
                new Country { Id = 16, Name = "Finland" },
                new Country { Id = 17, Name = "Greece" },
                new Country { Id = 18, Name = "Hungary" },
                new Country { Id = 19, Name = "Ireland" },
                new Country { Id = 20, Name = "Japan" },
                new Country { Id = 21, Name = "Netherlands" },
                new Country { Id = 22, Name = "Norway" },
                new Country { Id = 23, Name = "Poland" },
                new Country { Id = 24, Name = "Portugal" },
                new Country { Id = 25, Name = "Romania" },
                new Country { Id = 26, Name = "Sweden" },
                new Country { Id = 27, Name = "Switzerland" },
                new Country { Id = 28, Name = "Turkey" },
                new Country { Id = 29, Name = "Ukraine" },
                new Country { Id = 30, Name = "United Arab Emirates" }
            );
        }
    }
}
