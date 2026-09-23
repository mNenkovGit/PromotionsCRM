
using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data.Models;

namespace PromotionsCRM.Data
{
    public class PromotionsCrmDbContext: DbContext
    {
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Country> Countries {get;set;} = null!;
        public virtual DbSet<Customer> Customers {get;set;} = null!;
        public virtual DbSet<Product> Products {get;set; } = null!;
        public virtual DbSet<Promotion> Promotions {get;set; } = null!;
        public virtual DbSet<PromotionCountry> PromotionsCountries {get;set; } = null!;
        public virtual DbSet<Submission> Submissions {get;set; } = null!;
        public virtual DbSet<User> Users {get;set; } = null!;


        public PromotionsCrmDbContext()
        {
        }

        public PromotionsCrmDbContext(DbContextOptions<PromotionsCrmDbContext> options) : base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromotionCountry>()
                    .HasKey(pm => new
                    {
                        pm.PromotionId,
                        pm.CountryId
                    });

            modelBuilder.Entity<PromotionCountry>()
                .HasOne(pm => pm.Promotion)
                .WithMany(pm => pm.PromotionsCountries)
                .HasForeignKey(pm => pm.PromotionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PromotionCountry>()
                .HasOne(pm => pm.Country)
                .WithMany(pm => pm.PromotionsCountries)
                .HasForeignKey(pm => pm.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Submissions)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

          modelBuilder.Entity<Promotion>()
                .HasMany(pm => pm.Submissions)
                .WithOne(s => s.Promotion)
                .HasForeignKey(s => s.PromotionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany(pr => pr.Submissions)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Country>().HasData(
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

            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, Name = "Canon", Email = "canon@example.com", Address = "123 Canon Street", CountryId = 1, PhoneNumber = "123-456-7890", RegisteredOn = new DateTime(2026, 1, 15), Website = "www.canon.com" },
                   new Client { Id = 2, Name = "Nikon", Email = "nikon@example.com", Address = "456 Nikon Avenue", CountryId = 1, PhoneNumber = "987-654-3210", RegisteredOn = new DateTime(2026, 1, 15), Website = "www.nikon.com" },
                   new Client { Id = 3, Name = "Sony", Email = "sony@example.com", Address = "789 Sony Boulevard", CountryId = 1, PhoneNumber = "555-555-5555", RegisteredOn = new DateTime(2026, 1, 15), Website = "www.sony.com" }
            );

            modelBuilder.Entity<Promotion>().HasData(
                new Promotion { Id = 1, Name = "Canon Summer Sale", StartDate = new DateTime(2026, 6, 1), EndDate = new DateTime(2026, 8, 31), ClientId = 1 },
                new Promotion { Id = 2, Name = "Nikon Winter Clearance", StartDate = new DateTime(2026, 12, 1), EndDate = new DateTime(2027, 2, 28), ClientId = 2 },
                new Promotion { Id = 3, Name = "Sony Holiday Deals",  StartDate = new DateTime(2026, 11, 15), EndDate = new DateTime(2026, 12, 31), ClientId = 3 }
            );

        }
    }
}
