using System.Linq.Expressions;

namespace CMSoft.Framework.Linq
{
    /// <summary>
    /// Lambda Expression Builder
    /// </summary>
    public static class PredicateBuilder
    {
        /// <summary>
        /// Builder initializer to true
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <returns>Lambda expression</returns>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        /// <summary>
        /// Builder initializer to dalse
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <returns>Lambda expression</returns>
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        /// <summary>
        /// Allows joining two Lambda expressions with an OR condition
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="expr1">base lambda expression</param>
        /// <param name="expr2">Piece of lambda expression to add</param>
        /// <returns>new lambda expression</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        /// <summary>
        /// Allows joining two Lambda expressions with an AND condition
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="expr1">base lambda expression</param>
        /// <param name="expr2">Piece of lambda expression to add</param>
        /// <returns>new lambda expression</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
