using Microsoft.EntityFrameworkCore;
using PromotionsCRM.Data.Repository.Contracts.Common;

namespace PromotionsCRM.Data.Repository.Common
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {

        protected BaseRepository(PromotionsCrmDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        protected PromotionsCrmDbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; set; }

        public IQueryable<TEntity> All()
        {
            return DbSet.AsQueryable();
        }

        public IQueryable<TEntity> AllAsNoTracking()
        {
            return DbSet.AsNoTracking()
                    .AsQueryable();
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void AddRange(IEnumerable<TEntity> entityCollection)
        {
            DbSet.AddRange(entityCollection);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entityCollection)
        {
            await DbSet.AddRangeAsync(entityCollection);
        }


        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entityCollection)
        {
            DbSet.UpdateRange(entityCollection);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entityCollection)
        {
            DbSet.RemoveRange(entityCollection);
        }

        public async Task<TEntity?> FindByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

    }
}
