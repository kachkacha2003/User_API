﻿using MyWalletApi.Dtos;

namespace MyWalletApi.UserRepository
{
    public interface IUserRepository
    {
        Task AddUser(CreateUserDto createUserDto);
    }
}