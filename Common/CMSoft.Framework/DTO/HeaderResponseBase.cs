using System.Net;

namespace CMSoft.Framework.DTO
{
    /// <summary>
    /// Data Transfer Object - general state of the standard response
    /// </summary>
    public class HeaderResponseBase
    {
        /// <summary>
        /// Response Http Status Code
        /// </summary>
        public HttpStatusCode ResponseCode { get; set; }

        /// <summary>
        /// Response message 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Boolean request success indicator
        /// </summary>
        public bool Success
        {
            get
            {
                int responseCode = (int)ResponseCode;
                if (responseCode >= 200 && responseCode < 300)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Collect the errors (if it have someone)
        /// </summary>
        public IEnumerable<string>? Errors { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public HeaderResponseBase()
        {
            Message = "";
        }
    }
}
