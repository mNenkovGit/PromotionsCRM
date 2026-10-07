using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromotionsCRM.Data.Models;

namespace PromotionsCRM.Web.Data.Configurations
{
    public class PromotionCountryConfiguration : IEntityTypeConfiguration<PromotionCountry>
    {
        public void Configure(EntityTypeBuilder<PromotionCountry> builder)
        {
            builder.HasKey(pm => new
                    {
                        pm.PromotionId,
                        pm.CountryId
                    });

            builder.HasOne(pm => pm.Promotion)
                .WithMany(pm => pm.PromotionsCountries)
                .HasForeignKey(pm => pm.PromotionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pm => pm.Country)
                .WithMany(pm => pm.PromotionsCountries)
                .HasForeignKey(pm => pm.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
             builder.HasData(
                    new PromotionCountry { PromotionId = 1,  CountryId = 1 },  new PromotionCountry { PromotionId = 1,  CountryId = 4 },
                    new PromotionCountry { PromotionId = 1,  CountryId = 5 },  new PromotionCountry { PromotionId = 4,  CountryId = 1 },
                    new PromotionCountry { PromotionId = 4,  CountryId = 3 },  new PromotionCountry { PromotionId = 5,  CountryId = 2 },
                    new PromotionCountry { PromotionId = 5,  CountryId = 8 },  new PromotionCountry { PromotionId = 6,  CountryId = 1 },
                    new PromotionCountry { PromotionId = 7,  CountryId = 1 },  new PromotionCountry { PromotionId = 7,  CountryId = 4 },
                    new PromotionCountry { PromotionId = 7,  CountryId = 21 }, new PromotionCountry { PromotionId = 8,  CountryId = 7 },
                    new PromotionCountry { PromotionId = 8,  CountryId = 24 }, new PromotionCountry { PromotionId = 9,  CountryId = 1 },
                    new PromotionCountry { PromotionId = 9,  CountryId = 2 },  new PromotionCountry { PromotionId = 9,  CountryId = 3 },
                    new PromotionCountry { PromotionId = 9,  CountryId = 4 },  new PromotionCountry { PromotionId = 10, CountryId = 20 },
                    new PromotionCountry { PromotionId = 11, CountryId = 6 },  new PromotionCountry { PromotionId = 11, CountryId = 17 },
                    new PromotionCountry { PromotionId = 12, CountryId = 1 },  new PromotionCountry { PromotionId = 12, CountryId = 4 },
                    new PromotionCountry { PromotionId = 12, CountryId = 5 },  new PromotionCountry { PromotionId = 12, CountryId = 6 },
                    new PromotionCountry { PromotionId = 12, CountryId = 7 },  new PromotionCountry { PromotionId = 13, CountryId = 1 },
                    new PromotionCountry { PromotionId = 14, CountryId = 16 }, new PromotionCountry { PromotionId = 14, CountryId = 22 },
                    new PromotionCountry { PromotionId = 14, CountryId = 26 }, new PromotionCountry { PromotionId = 15, CountryId = 2 },
                    new PromotionCountry { PromotionId = 15, CountryId = 9 }
                );
        }
    }
}
