using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
	public class JWTTokenGenerator : IJWTTokenGenerator
	{
		private readonly IConfiguration _config;

		public JWTTokenGenerator(IConfiguration config)
		{
			_config = config;

		}
		public string GenerateToken(User user, DateTime expDate, bool isSubscriber)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.GivenName , user.UserName),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim("isSubscriber", isSubscriber.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = expDate,
				SigningCredentials = creds,
				Issuer = _config["Token:Issuer"],
				Audience = _config["Token:Audience"]
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}