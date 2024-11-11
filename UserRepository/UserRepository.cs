using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWalletApi.Data;
using MyWalletApi.Dtos;
using MyWalletApi.Models;
using MyWalletApi.Services;
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
        private readonly TokenService tokenService;

        public UserRepository(UserDbContext userDbContext,IMapper mapper,IConfiguration configuration, TokenService tokenService) {
            data = userDbContext;
            this.mapper = mapper;
            this.configuration= configuration;
            this.tokenService = tokenService;

            
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

       

       
    }
}
