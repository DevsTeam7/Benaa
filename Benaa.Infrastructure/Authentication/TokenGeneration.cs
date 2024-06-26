﻿using Benaa.Core.Entities.General;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Benaa.Core.Interfaces.Authentication
{
    public class TokenGeneration : ITokenGeneration
    {
        private readonly IConfiguration _config;
        private readonly ILogger<TokenGeneration> _logger;
        public TokenGeneration(IConfiguration config,
            ILogger<TokenGeneration> logger)
        {
            _config = config;
            _logger = logger;
        }

        public string GenerateTokenString(User user, IList<string> userRoles)
        {
            IList<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("418EBF8D-8968-4406-AAC1-9E7883BAA270"));
            SigningCredentials signinCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(3),
                issuer: "http://localhost:5084",
                audience: "http://localhost:5084",
                signingCredentials: signinCred
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            _logger.LogInformation("The Token is created", tokenString);

            return tokenString;
        }
    }
}
