using Microsoft.EntityFrameworkCore;

namespace CMSoft.Framework
{
    /// <summary>
    /// Create a repository manager for a model. You must specify the DbContext and the entity (as a type) that you want to manage.
    /// </summary>
    /// <typeparam name="TEntity">Entidad que se quiere administrar</typeparam>
    public class StandardModelImplementation<TEntity> : BaseModelImplementation<TEntity, DbContext> where TEntity : class, new()
    {
        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="context">Context to be managed</param>
        public StandardModelImplementation(DbContext context) : base(context)
        {
            _db = context;
            _model = _db.Set<TEntity>();
        }

      
    }
}