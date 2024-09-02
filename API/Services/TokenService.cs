using System;
using API.Entities;
using API.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace API.Services
{
	public class TokenService : ITokenService
	{
        private IConfiguration _config;

		public TokenService(IConfiguration config)
		{
            _config = config;
		}

        public string CreateToken(AppUser user)
        {
            var tokenKey = _config["TokenKey"] ?? throw new Exception("Token Key not found!");

            if (tokenKey.Length < 64)
            {
                throw new Exception("Token Key length must be 64 characters or greater. Token Key must be longer!");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };


            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

