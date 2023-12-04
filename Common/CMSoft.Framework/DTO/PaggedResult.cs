namespace CMSoft.Framework.DTO
{
    /// <summary>
    /// Show paginated results
    /// </summary>
    /// <typeparam name="T">Concrete class</typeparam>
    public class PaggedResult<T>
    {
        /// <summary>
        /// Total number of records
        /// </summary>
        public int FullResultCount { get; set; }

        /// <summary>
        /// Concrete object
        /// </summary>
        public T? Result { get; set; }
    }
}


