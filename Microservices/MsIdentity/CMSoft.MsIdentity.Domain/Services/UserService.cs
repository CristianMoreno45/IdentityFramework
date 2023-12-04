using CMSoft.Common.MsIdentity.Dtos;
using CMSoft.Framework.DTO;
using CMSoft.Framework.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CMSoft.MsIdentity.Domain.Services
{
    public interface IUserService
    {
        Task<string> ResgisterUserAsync(RegisterViewModel model);
        Task<string> GetUserTokenAsync(GetTokenRequest model);
    }
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }



        public async Task<string> ResgisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new InvalidModelException("The input data is null.");
            if (model.Password != model.ConfirmPassword)
                throw new InvalidModelException("Confirm password doesn't match the password.");

            var identityUser = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (!result.Succeeded)
                throw new IncompleteOperationException("User didn't create.", result.Errors.Select(e => e.Description));
            
            return "User created successfully";

        }

        public async Task<string> GetUserTokenAsync(GetTokenRequest model)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                throw new SecurityRuleException("There is no user whit Email address.");

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                throw new SecurityRuleException("Invalid password.");

            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"] ?? ""));


            if(!int.TryParse(_configuration["AuthSettings:ExpirationTokenMinutes"], out int minutes))
            {
                throw new BadConfigurationException("The configuration AuthSettings:ExpirationTokenMinutes has not been established.");
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;

        }
    }
}
