using System.Net;

namespace CMSoft.Framework.Exceptions
{
    /// <summary>
    /// Exception for general middleware request errors
    /// </summary>
    public class HttpResquestResponseException : Exception
    {
        public int StatusCode { get; set; }
        public HttpResquestResponseException()
        {
        }

        public HttpResquestResponseException(int statusCode, string message = "") : base(GetStatusDescription(statusCode) + " - " + message)
        {
            StatusCode = statusCode;
        }

        public HttpResquestResponseException(string message, int statusCode, Exception inner) : base(message, inner)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Get request status description
        /// </summary>
        /// <param name="statusCode">Status code</param>
        /// <returns>Description</returns>
        public static string GetStatusDescription(int statusCode)
        {
            switch (statusCode)
            {
                case (int)HttpStatusCode.Continue: return "Continue";
                case (int)HttpStatusCode.SwitchingProtocols: return "SwitchingProtocols";
                case (int)HttpStatusCode.Processing: return "Processing";
                case (int)HttpStatusCode.EarlyHints: return "EarlyHints";
                case (int)HttpStatusCode.OK: return "OK";
                case (int)HttpStatusCode.Created: return "Created";
                case (int)HttpStatusCode.Accepted: return "Accepted";
                case (int)HttpStatusCode.NonAuthoritativeInformation: return "NonAuthoritativeInformation";
                case (int)HttpStatusCode.NoContent: return "NoContent";
                case (int)HttpStatusCode.ResetContent: return "ResetContent";
                case (int)HttpStatusCode.PartialContent: return "PartialContent";
                case (int)HttpStatusCode.MultiStatus: return "MultiStatus";
                case (int)HttpStatusCode.AlreadyReported: return "AlreadyReported";
                case (int)HttpStatusCode.IMUsed: return "IMUsed";
                case (int)HttpStatusCode.Ambiguous: return "Ambiguous";
                case (int)HttpStatusCode.Moved: return "Moved";
                case (int)HttpStatusCode.Redirect: return "Redirect";
                case (int)HttpStatusCode.RedirectMethod: return "RedirectMethod";
                case (int)HttpStatusCode.NotModified: return "NotModified";
                case (int)HttpStatusCode.UseProxy: return "UseProxy";
                case (int)HttpStatusCode.Unused: return "Unused";
                case (int)HttpStatusCode.RedirectKeepVerb: return "RedirectKeepVerb";
                case (int)HttpStatusCode.PermanentRedirect: return "PermanentRedirect";
                case (int)HttpStatusCode.BadRequest: return "BadRequest";
                case (int)HttpStatusCode.Unauthorized: return "Unauthorized";
                case (int)HttpStatusCode.PaymentRequired: return "PaymentRequired";
                case (int)HttpStatusCode.Forbidden: return "Forbidden";
                case (int)HttpStatusCode.NotFound: return "NotFound";
                case (int)HttpStatusCode.MethodNotAllowed: return "MethodNotAllowed";
                case (int)HttpStatusCode.NotAcceptable: return "NotAcceptable";
                case (int)HttpStatusCode.ProxyAuthenticationRequired: return "ProxyAuthenticationRequired";
                case (int)HttpStatusCode.RequestTimeout: return "RequestTimeout";
                case (int)HttpStatusCode.Conflict: return "Conflict";
                case (int)HttpStatusCode.Gone: return "Gone";
                case (int)HttpStatusCode.LengthRequired: return "LengthRequired";
                case (int)HttpStatusCode.PreconditionFailed: return "PreconditionFailed";
                case (int)HttpStatusCode.RequestEntityTooLarge: return "RequestEntityTooLarge";
                case (int)HttpStatusCode.RequestUriTooLong: return "RequestUriTooLong";
                case (int)HttpStatusCode.UnsupportedMediaType: return "UnsupportedMediaType";
                case (int)HttpStatusCode.RequestedRangeNotSatisfiable: return "RequestedRangeNotSatisfiable";
                case (int)HttpStatusCode.ExpectationFailed: return "ExpectationFailed";
                case (int)HttpStatusCode.MisdirectedRequest: return "MisdirectedRequest";
                case (int)HttpStatusCode.UnprocessableEntity: return "UnprocessableEntity";
                case (int)HttpStatusCode.Locked: return "Locked";
                case (int)HttpStatusCode.FailedDependency: return "FailedDependency";
                case (int)HttpStatusCode.UpgradeRequired: return "UpgradeRequired";
                case (int)HttpStatusCode.PreconditionRequired: return "PreconditionRequired";
                case (int)HttpStatusCode.TooManyRequests: return "TooManyRequests";
                case (int)HttpStatusCode.RequestHeaderFieldsTooLarge: return "RequestHeaderFieldsTooLarge";
                case (int)HttpStatusCode.UnavailableForLegalReasons: return "UnavailableForLegalReasons";
                case (int)HttpStatusCode.InternalServerError: return "InternalServerError";
                case (int)HttpStatusCode.NotImplemented: return "NotImplemented";
                case (int)HttpStatusCode.BadGateway: return "BadGateway";
                case (int)HttpStatusCode.ServiceUnavailable: return "ServiceUnavailable";
                case (int)HttpStatusCode.GatewayTimeout: return "GatewayTimeout";
                case (int)HttpStatusCode.HttpVersionNotSupported: return "HttpVersionNotSupported";
                case (int)HttpStatusCode.VariantAlsoNegotiates: return "VariantAlsoNegotiates";
                case (int)HttpStatusCode.InsufficientStorage: return "InsufficientStorage";
                case (int)HttpStatusCode.LoopDetected: return "LoopDetected";
                case (int)HttpStatusCode.NotExtended: return "NotExtended";
                case (int)HttpStatusCode.NetworkAuthenticationRequired: return "NetworkAuthenticationRequired";
                default:
                    return "Impossible";
            }
        }
    }
}
