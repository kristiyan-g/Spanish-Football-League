namespace Spanish.Football.League.Repository
{
    using Microsoft.EntityFrameworkCore;
    using Spanish.Football.League.Database;
    using Spanish.Football.League.DomainModels;

    /// <inheritdoc />
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : class
    {
        private readonly SpanishFootballLeagueDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="dbContext">An instance of db context.</param>
        public GenericRepository(SpanishFootballLeagueDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        /// <inheritdoc />
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <inheritdoc />
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        /// <inheritdoc />
        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <inheritdoc />
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        /// <inheritdoc />
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <inheritdoc />
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync()
        {
            SetCreatedDate();
            await _dbContext.SaveChangesAsync();
        }

        private void SetCreatedDate()
        {
            var entries = _dbContext.ChangeTracker.Entries()
                .Where(e => e.Entity is BaseModel<TKey> && e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                var entity = (BaseModel<TKey>)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = DateTime.UtcNow;
                }
            }
        }
    }
}