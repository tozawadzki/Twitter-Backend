namespace PGSTwitter.Services.Implementations
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Repositories.Models;
    using UserModels;

    public class TokenService : ITokenService
    {
        private readonly UserManager<TwitterUser> _userManager;
        private readonly IConfiguration _configuration;

        public TokenService(UserManager<TwitterUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GetToken(LoginDataDTO loginDataDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDataDto.Email);
            if (user == null)
            {
                return null;
            }

            bool passwordCorrect = await _userManager.CheckPasswordAsync(user, loginDataDto.Password);
            if (!passwordCorrect)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtToken:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(
                    _configuration.GetValue<int>("JwtToken:ExpirationMinutes")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateEncodedJwt(tokenDescriptor);

            return token;
        }
    }
}
