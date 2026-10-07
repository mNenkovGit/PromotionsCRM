using PromotionsCRM.Data.Enums;
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
                .OnDelete(DeleteBehavior.Cascade);

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
             modelBuilder.Entity<Product>().HasData(
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

            modelBuilder.Entity<PromotionCountry>().HasData(
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

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Ivan",   LastName = "Petrov",  Email = "ivan.petrov@example.com",   PhoneNumber = "359-888-1234", CountryId = 1,  RegisteredOn = new DateTime(2026, 2, 10) },
                new Customer { Id = 2, FirstName = "Maria",  LastName = "Georgieva", Email = "maria.g@example.com",     PhoneNumber = "359-877-5678", CountryId = 1,  RegisteredOn = new DateTime(2026, 3, 5) },
                new Customer { Id = 3, FirstName = "James",  LastName = "Smith",   Email = "james.smith@example.com",   PhoneNumber = "447-700-9001", CountryId = 3,  RegisteredOn = new DateTime(2026, 4, 18) },
                new Customer { Id = 4, FirstName = "Anna",   LastName = "Muller",  Email = "anna.muller@example.com",   PhoneNumber = "491-512-3456", CountryId = 4,  RegisteredOn = new DateTime(2026, 1, 22) },
                new Customer { Id = 5, FirstName = "Kenji",  LastName = "Tanaka",  Email = "kenji.tanaka@example.com",  PhoneNumber = "819-012-3456", CountryId = 20, RegisteredOn = new DateTime(2026, 5, 30) }
            );

            modelBuilder.Entity<Submission>().HasData(
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
