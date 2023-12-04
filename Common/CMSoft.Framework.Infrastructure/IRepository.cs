using System.Linq.Expressions;

namespace CMSoft.Framework
{
    /// <summary>
    /// Base repository contract for database access
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<T> where T : class
    {
        #region Methods
        /// <summary>
        /// Method that allows adding an entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>Task</returns>
        T Add(T entity, bool saveChanges = true);

        /// <summary>
        /// Method that allows adding multiple entities
        /// </summary>
        /// <param name="entities">Entities to add</param>
        /// <returns>Task</returns>
        IEnumerable<T> Add(IEnumerable<T> entities, bool saveChanges = true);

        /// <summary>
        /// Method that allows adding multiple entities asynchronously
        /// </summary>
        /// <param name="entities">Entities to add</param>
        /// <returns>Task</returns>
        Task AddAsync(IEnumerable<T> entities);

        /// <summary>
        /// Method that allows adding only one entity asynchronously
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <returns>Task</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Method that allows deleting an entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(T entity, bool saveChanges = true);

        /// <summary>
        /// Method that allows multiple entities
        /// </summary>
        /// <param name="entities">Entities to delete</param>
        void Delete(IEnumerable<T> entities, bool saveChanges = true);

        /// <summary>
        /// Method that gets all database objects
        /// </summary>
        /// <returns>IQueryable of the Entity</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Method that obtains all database objects according to a filter
        /// </summary>
        /// <param name="filter">Lamda expresion to filter</param>
        /// <returns>IQueryable of the Entity</returns>
        IQueryable<T> GetByFilter(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Method that obtains an entity from its id
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Entidad</returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Method that stores changes in the database
        /// </summary>
        /// <returns>Task</returns>
        void SaveChanges();

        /// <summary>
        /// Method that stores changes in the database asynchronously
        /// </summary>
        /// <returns>Task</returns>
        Task SaveChangesAsync();

        /// <summary>
        /// Allows you to create a database transaction and send as a parameter a delegate of the action to be executed
        /// </summary>
        /// <param name="dataBaseOperations">Delegate of the action</param>
        void StartOwnTransactionWithinContext(Action dataBaseOperations);

        #endregion
    }
}