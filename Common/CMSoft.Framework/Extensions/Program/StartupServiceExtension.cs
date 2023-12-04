using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CMSoft.Framework.Extensions.Program
{
    /// <summary>
    /// Extension class that allows the injection of behaviors to the Service collection on StartUp/Program file
    /// </summary>
    public static class StartupServiceExtension
    {
        /// <summary>
        /// Allow to set a physical policy
        /// </summary>
        /// <param name="service">Target</param>
        /// <param name="policyName">Policy name</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection SetPhysicalPolicy(this IServiceCollection service, string policyName)
        {
            service.AddCors(o => o.AddPolicy(policyName, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            return service;
        }

        /// <summary>
        /// Allow to set Bearer Authentication
        /// </summary>
        /// <param name="service">Target</param>
        /// <param name="authorityUrl">Authority Url</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddBearerAuthentication(this IServiceCollection service, string authorityUrl)
        {
            service
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = authorityUrl;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
            return service;
        }

        /// <summary>
        /// Allow to set a logical policy (by scopes)
        /// </summary>
        /// <param name="service">Target</param>
        /// <param name="serviceName">Service domain name</param>
        /// <param name="scopes">Scope set to authority Url</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddScopesPolicy(this IServiceCollection service, string serviceName, List<string> scopes)
        {
            service
               .AddAuthorization(options =>
               {
                   foreach (var scope in scopes)
                   {
                       options.AddPolicy(scope, policy => policy.RequireClaim("scope", serviceName + "." + scope));
                   }

               });
            return service;
        }

        /// <summary>
        /// Configure JSON Objects behavior preferences
        /// </summary>
        /// <param name="service">Target</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection ConfigureJsonPreferences(this IServiceCollection service)
        {
            service.Configure(delegate (JsonOptions options)
            {
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.SerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.SerializerOptions.AllowTrailingCommas = true;
            });
            return service;
        }
    }

}
