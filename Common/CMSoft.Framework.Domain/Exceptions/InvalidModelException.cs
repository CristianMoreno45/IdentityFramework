namespace CMSoft.Framework.Domain.Exceptions
{
    /// <summary>
    /// Business Rule Validation Exception
    /// </summary>
    public class InvalidModelException : Exception
    {
        public InvalidModelException(string error) : base(error)
        {
        }

        public InvalidModelException(List<string> errors) : base(string.Join(" ", errors))
        {
        }
    }
}
