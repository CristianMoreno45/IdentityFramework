using CMSoft.Common.MsIdentity.Dtos;
using CMSoft.Framework.DTO;
using CMSoft.Framework.Exceptions;
using CMSoft.MsIdentity.Domain.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMSoft.MsIdentity.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;
        private readonly IAuthenticationHandlerProvider _authenticationHandlerProvider;


        public AuthController(IUserService userService, IAuthenticationSchemeProvider authenticationSchemeProvider, IAuthenticationHandlerProvider authenticationHandlerProvider)
        {
            _userService = userService;
            _authenticationSchemeProvider = authenticationSchemeProvider;
            _authenticationHandlerProvider = authenticationHandlerProvider;

        }


        [HttpPost]
        [Route("getusertoken")]
        public async Task<IActionResult> GetUserTokenAsync([FromBody] GetTokenRequest model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseBase()
                    {
                        Header = new HeaderResponseBase()
                        {
                            Message = "Some properties are not valid.",
                            ResponseCode = System.Net.HttpStatusCode.BadRequest
                        }
                    });
                }

                var result = await _userService.GetUserTokenAsync(model);

                if (string.IsNullOrEmpty(result))
                {
                    throw new IncompleteOperationException(MessagesError.GENERAL_ERROR);
                }

                return Ok(new ResponseBase<string>()
                {
                    Header = new HeaderResponseBase()
                    {
                        Message = "Ok",
                        ResponseCode = System.Net.HttpStatusCode.OK
                    },
                    Data = result
                });
            }
            catch (Exception ex)
            {
                ResponseBase response = new ResponseBase()
                {
                    Header = new HeaderResponseBase() { Message = ex.Message }
                };
                switch (ex)
                {
                    case IncompleteOperationException:
                        response.Header.Errors = ((IncompleteOperationException)ex).Errors;
                        response.Header.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                        break;
                    case BadConfigurationException:
                        response.Header.Message = MessagesError.GENERAL_ERROR;
                        response.Header.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                        break;
                    case SecurityRuleException:
                        response.Header.ResponseCode = System.Net.HttpStatusCode.Unauthorized;
                        break;
                    default:
                        response.Header.ResponseCode = System.Net.HttpStatusCode.InternalServerError;
                        break;
                }
                return BadRequest(response);
            }

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseBase()
                    {
                        Header = new HeaderResponseBase()
                        {
                            Message = "Some properties are not valid.",
                            ResponseCode = System.Net.HttpStatusCode.BadRequest
                        }
                    });
                }

                var result = await _userService.ResgisterUserAsync(model);

                if (string.IsNullOrEmpty(result))
                {
                    throw new IncompleteOperationException(MessagesError.GENERAL_ERROR);
                }

                return Ok(new ResponseBase()
                {
                    Header = new HeaderResponseBase()
                    {
                        Message = result,
                        ResponseCode = System.Net.HttpStatusCode.OK
                    }
                });
            }

            catch (Exception ex)
            {
                ResponseBase response = new ResponseBase()
                {
                    Header = new HeaderResponseBase() { Message = ex.Message }
                };
                switch (ex)
                {
                    case IncompleteOperationException:
                        response.Header.Errors = ((IncompleteOperationException)ex).Errors;
                        response.Header.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                        break;
                    case InvalidModelException:
                        response.Header.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.Header.ResponseCode = System.Net.HttpStatusCode.InternalServerError;
                        break;
                }
                return BadRequest(response);
            }
        }
        [HttpGet("external-login/{provider}")]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var authenticationScheme = _authenticationSchemeProvider.GetSchemeAsync(provider).Result;
            if (authenticationScheme == null)
            {
                return BadRequest("Invalid authentication provider.");
            }

            var properties = new AuthenticationProperties { RedirectUri = returnUrl };
            var handler = _authenticationHandlerProvider.GetHandlerAsync(HttpContext, provider);
            if (handler == null)
            {
                return BadRequest("Invalid authentication handler.");
            }
            try
            {
                return Challenge(properties, provider);
            }
            catch (Exception ex )
            {

                ResponseBase response = new ResponseBase()
                {
                    Header = new HeaderResponseBase() { Message = ex.Message }
                };
                switch (ex)
                {
                    case IncompleteOperationException:
                        response.Header.Errors = ((IncompleteOperationException)ex).Errors;
                        response.Header.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                        break;
                    case InvalidModelException:
                        response.Header.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.Header.ResponseCode = System.Net.HttpStatusCode.InternalServerError;
                        break;
                }
                return BadRequest(response);
            }
          

        }
        [HttpGet("external-login-callback")]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            if (remoteError != null)
            {
                // Manejar error remoto
                return BadRequest($"Remote error: {remoteError}");
            }

            var info = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);

            if (info == null)
            {
                // Manejar error de autenticación externa
                return BadRequest("External authentication error");
            }

            // Aquí puedes acceder a la información del usuario obtenida del proveedor externo
            var externalUserId = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var fullName = info.Principal.FindFirstValue(ClaimTypes.Name);

            // Puedes realizar procesamiento adicional, como registrar el usuario en tu base de datos

            // Sign in the user with this external login provider if the user already has a login
            // or sign up a new user if this is their first time using the external login
            // Esto puede implicar registrar al usuario en tu base de datos y autenticarlo en tu aplicación

            // Sign in the user using the external login provider and navigate to the returnUrl
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, info.Principal);

            // Devuelve un resultado 200 OK u otra respuesta según tus necesidades
            return Ok(new { Message = "Authentication successful", ReturnUrl = returnUrl });
        }
    }
}
