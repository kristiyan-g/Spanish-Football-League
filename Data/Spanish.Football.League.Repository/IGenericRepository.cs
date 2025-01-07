namespace Spanish.Football.League.Repository
{
    /// <summary>
    /// Custom repository that allows database communication.
    /// </summary>
    /// <typeparam name="TEntity">Template entity of type class.</typeparam>
    /// <typeparam name="TKey">Template PK type.</typeparam>
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : class
    {
        /// <summary>
        /// Retrieves all entities of type TEntity from the database as an IQueryable,
        /// allowing for further filtering and querying before execution.
        /// </summary>
        /// <returns>An IQueryable collection of TEntity.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Asynchronously retrieves an entity of type TEntity by its primary key.
        /// </summary>
        /// <param name="id">The primary key value of the entity to retrieve, of type TKey.</param>
        /// <returns>
        /// A Task representing the asynchronous operation, with a result of the entity of type TEntity
        /// if found; otherwise, null if no entity with the specified key is found.
        /// </returns>
        /// <remarks>
        /// This method uses EF Core's FindAsync, which is optimized for primary key lookups.
        /// If the entity is already being tracked by the context, it will be returned directly
        /// from the cache, otherwise a database query will be executed.
        /// </remarks>
        Task<TEntity?> GetByIdAsync(TKey id);

        /// <summary>
        /// Asynchronously adds a new entity of type TEntity to the database.
        /// This method does not immediately save changes to the database.
        /// </summary>
        /// <param name="entity">The entity instance to be added.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity of type TEntity in the database.
        /// This method marks the entity as modified, but does not immediately save changes.
        /// </summary>
        /// <param name="entity">The entity instance to be updated.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an existing entity of type TEntity from the database.
        /// This method does not immediately save changes.
        /// </summary>
        /// <param name="entity">The entity instance to be deleted.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Asynchronously add multiple entities to the database context or repository at once,
        /// improving performance by reducing the number of database operations.
        /// </summary>
        /// <param name="entities">The entities instance to be added.</param>
        /// <returns>A Task representing the asynchronous save operation.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Asynchronously saves all pending changes to the database.
        /// This method should be called after AddAsync, Update, or Delete to persist changes.
        /// </summary>
        /// <returns>A Task representing the asynchronous save operation.</returns>
        Task SaveChangesAsync();
    }
}
