using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromotionsCRM.Data.Enums;
using PromotionsCRM.Data.Models;

namespace PromotionsCRM.Web.Data.Configurations
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
             builder
                .HasOne(s => s.Customer)
                .WithMany(c => c.Submissions)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
           builder.HasData(
            new Submission { Id = 1,  CustomerId = 1, PromotionId = 1,  ProductId = 1,  Status = StatusName.Valid,          PurchaseDate = new DateTime(2026, 6, 10), SubmittedOn = new DateTime(2026, 6, 12), ProcessedOn = new DateTime(2026, 6, 20) },
            new Submission { Id = 2,  CustomerId = 2, PromotionId = 1,  ProductId = 2,  Status = StatusName.Invalid,        PurchaseDate = new DateTime(2026, 7, 5),  SubmittedOn = new DateTime(2026, 7, 6),  ProcessedOn = new DateTime(2026, 7, 15) },
            new Submission { Id = 3,  CustomerId = 3, PromotionId = 4,  ProductId = 3,  Status = StatusName.BeingProcessed, PurchaseDate = new DateTime(2026, 9, 1),  SubmittedOn = new DateTime(2026, 9, 3) },
            new Submission { Id = 4,  CustomerId = 1, PromotionId = 4,  ProductId = 4,  Status = StatusName.Valid,          PurchaseDate = new DateTime(2026, 8, 20), SubmittedOn = new DateTime(2026, 8, 22), ProcessedOn = new DateTime(2026, 8, 30) },
            new Submission { Id = 5,  CustomerId = 4, PromotionId = 6,  ProductId = 7,  Status = StatusName.Valid,          PurchaseDate = new DateTime(2026, 3, 15), SubmittedOn = new DateTime(2026, 3, 18), ProcessedOn = new DateTime(2026, 3, 25) },
            new Submission { Id = 6,  CustomerId = 5, PromotionId = 7,  ProductId = 9,  Status = StatusName.BeingProcessed, PurchaseDate = new DateTime(2026, 9, 10), SubmittedOn = new DateTime(2026, 9, 12) },
            new Submission { Id = 7,  CustomerId = 2, PromotionId = 7,  ProductId = 10, Status = StatusName.MissingInfo,    PurchaseDate = new DateTime(2026, 9, 25), SubmittedOn = new DateTime(2026, 9, 28), ProcessedOn = new DateTime(2026, 9, 30) },
            new Submission { Id = 8,  CustomerId = 3, PromotionId = 8,  ProductId = 11, Status = StatusName.Valid,          PurchaseDate = new DateTime(2026, 7, 1),  SubmittedOn = new DateTime(2026, 7, 3),  ProcessedOn = new DateTime(2026, 7, 10) },
            new Submission { Id = 9,  CustomerId = 4, PromotionId = 10, ProductId = 14, Status = StatusName.Cancelled,      PurchaseDate = new DateTime(2026, 9, 21), SubmittedOn = new DateTime(2026, 9, 24), ProcessedOn = new DateTime(2026, 9, 25) },
            new Submission { Id = 10, CustomerId = 5, PromotionId = 11, ProductId = 16, Status = StatusName.Invalid,        PurchaseDate = new DateTime(2026, 4, 5),  SubmittedOn = new DateTime(2026, 4, 8),  ProcessedOn = new DateTime(2026, 4, 15) }
            );    
        }
    }
}
