using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data.Models;

namespace PromotionsCRM.Web.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {

             builder.HasMany(pr => pr.Submissions)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(
                    new Product { Id = 1,  Name = "Canon EOS R6 Mark II",     PromotionId = 1 },
                    new Product { Id = 2,  Name = "Canon RF 24-70mm f/2.8",   PromotionId = 1 },
                    new Product { Id = 3,  Name = "Canon PIXMA G650",         PromotionId = 4 },
                    new Product { Id = 4,  Name = "Canon i-SENSYS MF655Cdw",  PromotionId = 4 },
                    new Product { Id = 5,  Name = "Canon EOS R8",             PromotionId = 5 },
                    new Product { Id = 6,  Name = "Canon PowerShot V10",      PromotionId = 5 },
                    new Product { Id = 7,  Name = "Canon RF 50mm f/1.8",      PromotionId = 6 },
                    new Product { Id = 8,  Name = "Canon SELPHY CP1500",      PromotionId = 13 },
                    new Product { Id = 9,  Name = "Nikon Z6 III",             PromotionId = 7 },
                    new Product { Id = 10, Name = "Nikon NIKKOR Z 24-120mm",  PromotionId = 7 },
                    new Product { Id = 11, Name = "Nikon COOLPIX P950",       PromotionId = 8 },
                    new Product { Id = 12, Name = "Nikon Z fc",               PromotionId = 9 },
                    new Product { Id = 13, Name = "Nikon Z 50mm f/1.8 S",     PromotionId = 14 },
                    new Product { Id = 14, Name = "Sony WH-1000XM5",          PromotionId = 10 },
                    new Product { Id = 15, Name = "Sony WF-1000XM5",          PromotionId = 10 },
                    new Product { Id = 16, Name = "Sony SRS-XB100",           PromotionId = 11 },
                    new Product { Id = 17, Name = "Sony Alpha 7 IV",          PromotionId = 12 },
                    new Product { Id = 18, Name = "Sony ULT Field 1",         PromotionId = 15 }
                );
        }
    }
}
