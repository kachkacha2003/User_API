using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWalletApi.Data;
using MyWalletApi.Dtos;
using MyWalletApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWalletApi.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext data;
        private readonly IMapper mapper;
        private readonly IConfiguration  configuration;

        public UserRepository(UserDbContext userDbContext,IMapper mapper,IConfiguration configuration) {
            data = userDbContext;
            this.mapper = mapper;
            this.configuration= configuration;

            
        }
        public async Task AddUser(CreateUserDto createUserDto)
        {
            var user=mapper.Map<User>(createUserDto);
            user.Password=BCrypt.Net.BCrypt.HashPassword(user.Password);
            await data.Users.AddAsync(user);
            await data.SaveChangesAsync();
          
        }

        public async Task DeleteUser(User user)
        {
           
                data.Users.Remove(user);
                await data.SaveChangesAsync();
            
           
        }

        public async Task<List<UserDto>> GetAllUsers()
        {

            var users = await data.Users.ToListAsync();
            return mapper.Map<List<UserDto>>(users);
        }

        public string GenerateToken(User user)
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
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> LoginUser(LoginUserDto loginUser)
        {
            var user = await data.Users.FirstOrDefaultAsync(x => x.Username == loginUser.Username);

            if (user != null && BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            {
              
                return GenerateToken(user);
            }
            else
            {
                return "";
            }
        }
    }
}
