using FluentValidation;
using FluentValidation.Results;
using CMSoft.Framework.Domain.Exceptions;

namespace CMSoft.Framework.Domain.ModelValidation
{
    /// <summary>
    /// Extension to FluentValidation
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Validates the model using FluentValidation and throws an InvalidModelException if the validation fails.
        /// </summary>
        /// <typeparam name="T">The type of the model to be validated.</typeparam>
        /// <param name="validator">The instance of the FluentValidation validator.</param>
        /// <param name="model">The model instance to validate.</param>
        /// <param name="ruleSet">The set of ValidationTypeEnum rules to apply during validation.</param>
        /// <exception cref="InvalidModelException">Thrown if model validation fails.</exception>
        public static void ValidateAndThrow<T>(this IValidator<T> validator, T model, string ruleSet)
        {
            ValidationResult result = validator.Validate(model, options => options.IncludeRuleSets(ruleSet));
            if (!result.IsValid)
            {
                InvalidModelException ex = new(result.Errors.Select(x => x.ErrorMessage).ToList());
                throw ex;
            }
        }
    }
}
