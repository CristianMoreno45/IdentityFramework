using System.Net;

namespace CMSoft.Framework.DTO
{
    /// <summary>
    /// Object extension for generic element conversion in ResponseBase
    /// </summary>
    public static class ResponseExtensions
    {
        /// <summary>
        /// Convert T in <see cref="ResponseBase"/>
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="resultDTO">Conversion goal object</param>
        /// <param name="code">Status code (default value is 200)</param>
        /// <param name="message">Response message (default value is 'OK')</param>
        /// <returns>Object converted as a <see cref="ResponseBase"/></returns>
        public static ResponseBase<T> AsResponseDTO<T>(this T resultDTO, HttpStatusCode code = HttpStatusCode.OK, string message = "OK")
        {
            ResponseBase<T> responseDTO = new();
            responseDTO.Data = resultDTO;
            responseDTO.Header = new HeaderResponseBase
            {
                ResponseCode = code,
                Message = message
            };
            return responseDTO;
        }
    }
}
