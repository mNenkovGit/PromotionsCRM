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
            

           

          

           

          modelBuilder.ApplyConfigurationsFromAssembly(typeof(PromotionsCrmDbContext).Assembly);
        }
    }
}
