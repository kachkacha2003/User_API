using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using MyWalletApi.Data;
using MyWalletApi.Dtos;
using MyWalletApi.Models;

namespace MyWalletApi.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext data;
        private readonly IMapper mapper;
        public UserRepository(UserDbContext userDbContext,IMapper mapper) {
            data = userDbContext;
            this.mapper = mapper;

            
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
