using MyWalletApi.Dtos;
using MyWalletApi.Models;
using System;

namespace MyWalletApi.UserRepository
{
    public interface IUserRepository
    {
        Task AddUser(CreateUserDto createUserDto);
        Task <List<UserDto>> GetAllUsers();
        Task DeleteUser(User id);
    }
}
