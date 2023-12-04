using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CMSoft.Framework
{
    /// <summary>
    /// Create a repository manager for a model. It allows you to specify the type of DbContext (so that the other entities in the context can be accessed) and the entity (as a type) that you want to manage.
    /// </summary>
    /// <typeparam name="TEntity">Entity to be managed</typeparam>
    /// <typeparam name="TDbContext">Context data type</typeparam>
    public class BaseModelImplementation<TEntity, TDbContext> : IRepository<TEntity> where TEntity : class, new() where TDbContext : DbContext
    {
        protected TDbContext _db;
        protected DbSet<TEntity> _model;

        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="context">Context to be managed</param>
        public BaseModelImplementation(TDbContext context)
        {
            _db = context;
            _model = _db.Set<TEntity>();
        }

        /// <summary>
        /// Method that allows adding an entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>Task</returns>
        public TEntity Add(TEntity entity, bool saveChanges = true)
        {
            _db.Add(entity);
            if (saveChanges)
                _db.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Method that allows adding multiple entities
        /// </summary>
        /// <param name="entities">Entities to add</param>
        /// <returns>Task</returns>
        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            _model.AddRange(entities);
            if (saveChanges)
                _db.SaveChanges();
            return entities;
        }

        /// <summary>
        /// Method that allows adding multiple entities asynchronously
        /// </summary>
        /// <param name="entities">Entities to add</param>
        /// <returns>Task</returns>
        public async Task AddAsync(IEnumerable<TEntity> entities)
        {
            await _model.AddRangeAsync(entities);
        }

        /// <summary>
        /// Method that allows adding only one entity asynchronously
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>Task</returns>
        public async Task AddAsync(TEntity entity)
        {
            await _model.AddAsync(entity);
        }

        /// <summary>
        /// Method that allows deleting an entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public void Delete(TEntity entity, bool saveChanges = true)
        {
            _model.Remove(entity);
            if (saveChanges)
                _db.SaveChanges();
        }

        /// <summary>
        /// Method that allows multiple entities
        /// </summary>
        /// <param name="entities">Entities to delete</param>
        public void Delete(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            _model.RemoveRange(entities);
            if (saveChanges)
                _db.SaveChanges();
        }

        /// <summary>
        /// Method that gets all database objects
        /// </summary>
        /// <returns>IQueryable of the Entity</returns>
        public IQueryable<TEntity> GetAll()
        {
            return _model;
        }

        /// <summary>
        /// Method that obtains all database objects according to a filter
        /// </summary>
        /// <param name="filter">Lamda expresion to filter</param>
        /// <returns>IQueryable of the Entity</returns>
        public IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            return _model.Where(filter);
        }

        /// <summary>
        /// Method that obtains an entity from its id
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Entidad</returns>
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _model.FindAsync(id);
        }

        /// <summary>
        /// Method that stores changes in the database
        /// </summary>
        /// <returns>Task</returns>
        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        /// <summary>
        /// Method that stores changes in the database asynchronously
        /// </summary>
        /// <returns>Task</returns>
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Allows you to create a database transaction and send as a parameter a delegate of the action to be executed
        /// </summary>
        /// <param name="dataBaseOperations">Delegate of the action</param>
        public void StartOwnTransactionWithinContext(Action dataBaseOperations)
        {
            using var dbContextTransaction = _db.Database.BeginTransaction();
            try
            {
                dataBaseOperations();
                _db.SaveChanges();
                dbContextTransaction.Commit();
            }
            catch (Exception)
            {
                dbContextTransaction.Rollback();
                throw;
            }
        }

    }
}