using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromotionsCRM.Data.Models;

namespace PromotionsCRM.Web.Data.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
           builder.HasMany(pm => pm.Submissions)
                .WithOne(s => s.Promotion)
                .HasForeignKey(s => s.PromotionId)
                .OnDelete(DeleteBehavior.Restrict);

             builder.HasData(
                new Promotion { Id = 1, Name = "Canon Summer Sale", StartDate = new DateTime(2026, 6, 1), EndDate = new DateTime(2026, 8, 31), ClientId = 1 },
                new Promotion { Id = 2, Name = "Nikon Winter Clearance", StartDate = new DateTime(2026, 12, 1), EndDate = new DateTime(2027, 2, 28), ClientId = 2 },
                new Promotion { Id = 3, Name = "Sony Holiday Deals",  StartDate = new DateTime(2026, 11, 15), EndDate = new DateTime(2026, 12, 31), ClientId = 3 },
                new Promotion { Id = 4,  Name = "Canon Back to School",      StartDate = new DateTime(2026, 8, 15),  EndDate = new DateTime(2026, 9, 30),  ClientId = 1 },
                new Promotion { Id = 5,  Name = "Canon Black Friday",        StartDate = new DateTime(2026, 11, 20), EndDate = new DateTime(2026, 11, 30), ClientId = 1 },
                new Promotion { Id = 6,  Name = "Canon Spring Photo Fest",   StartDate = new DateTime(2026, 3, 1),   EndDate = new DateTime(2026, 4, 15),  ClientId = 1 },
                new Promotion { Id = 7,  Name = "Nikon Autumn Lens Offer",   StartDate = new DateTime(2026, 9, 1),   EndDate = new DateTime(2026, 10, 31), ClientId = 2 },
                new Promotion { Id = 8,  Name = "Nikon Summer Adventure",    StartDate = new DateTime(2026, 6, 15),  EndDate = new DateTime(2026, 8, 31),  ClientId = 2 },
                new Promotion { Id = 9,  Name = "Nikon New Year Bundle",     StartDate = new DateTime(2026, 12, 26), EndDate = new DateTime(2027, 1, 31),  ClientId = 2 },
                new Promotion { Id = 10, Name = "Sony Audio Week",           StartDate = new DateTime(2026, 9, 20),  EndDate = new DateTime(2026, 9, 27),  ClientId = 3 },
                new Promotion { Id = 11, Name = "Sony Easter Promo",         StartDate = new DateTime(2026, 4, 1),   EndDate = new DateTime(2026, 4, 20),  ClientId = 3 },
                new Promotion { Id = 12, Name = "Sony Winter Cashback",      StartDate = new DateTime(2027, 1, 10),  EndDate = new DateTime(2027, 2, 28),  ClientId = 3 },
                new Promotion { Id = 13, Name = "Canon Valentine's Print Deal", StartDate = new DateTime(2027, 2, 1),  EndDate = new DateTime(2027, 2, 14), ClientId = 1 },
                new Promotion { Id = 14, Name = "Nikon Spring Wildlife Promo",  StartDate = new DateTime(2027, 3, 15), EndDate = new DateTime(2027, 4, 30), ClientId = 2 },
                new Promotion { Id = 15, Name = "Sony Summer Sound Festival",   StartDate = new DateTime(2027, 6, 1),  EndDate = new DateTime(2027, 7, 15), ClientId = 3 }
            );
        }
    }
}
