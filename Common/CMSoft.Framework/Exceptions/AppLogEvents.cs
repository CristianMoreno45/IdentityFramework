using Microsoft.Extensions.Logging;

namespace CMSoft.Framework.Exceptions
{
    /// <summary>
    /// Class to define different events
    /// </summary>
    public static class AppLogEvents
    {
        public static readonly EventId Create = new(1000, "Created");
        public static readonly EventId Read = new(1001, "Read");
        public static readonly EventId Update = new(1002, "Updated");
        public static readonly EventId Delete = new(1003, "Deleted");

        // These are also valid EventId instances, as there's
        // an implicit conversion from int to an EventId
        public const int Details = 3000;
        public const int Error = 3001;

        public static readonly EventId ReadNotFound = 4000;
        public static readonly EventId UpdateNotFound = 4001;

        // ...
    }
}
