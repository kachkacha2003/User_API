using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWalletApi.Data;
using MyWalletApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWalletApi.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserDbContext _context;

        public TokenService(IConfiguration configuration, UserDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

      
        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            if (Environment.GetEnvironmentVariable("Key") == null)
            {
                throw new Exception();
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
        public async Task<RefreshToken> GenerateRefreshTokenAsync(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                ExpirationDate = DateTime.Now.AddDays(7),
                UserId = user.Id
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }

        
        public async Task<string> RefreshAccessTokenAsync(string refreshTokenFromRequest)
        {
            var refreshToken = await _context.RefreshTokens
       .Include(rt => rt.User) 
       .FirstOrDefaultAsync(rt => rt.Token == refreshTokenFromRequest);

            if (refreshToken == null || refreshToken.ExpirationDate < DateTime.Now)
            {
                throw new SecurityTokenException("Refresh token expired or invalid");
            }

            var newAccessToken = GenerateAccessToken(refreshToken.User);

          
            var newRefreshToken = await GenerateRefreshTokenAsync(refreshToken.User);

            
            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();

            return newAccessToken;
        }
    }
}
