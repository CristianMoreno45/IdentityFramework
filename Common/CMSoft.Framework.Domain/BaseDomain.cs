using FluentValidation;
using Microsoft.Extensions.Logging;

namespace CMSoft.Framework.Domain
{

    /// <summary>
    /// Base business (DDD) model
    /// </summary>
    abstract public class BaseDomain<AggregatesModel, RepositoryModel>
    {
        protected RepositoryModel _repository;
        protected IValidator<AggregatesModel> _validator;
        protected ILogger _logger;

        /// <summary>
        /// Create an instance of the class like a POCO object
        /// </summary>
        public BaseDomain()
        {

        }

        /// <summary>
        /// Force the repository
        /// </summary>
        /// <param name="repository">Entity in charge of the data model (repository)</param>
        public void SetRepository(RepositoryModel repository)
        {
            _repository = repository;
        }
    }
}