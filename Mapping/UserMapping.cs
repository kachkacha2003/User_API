using AutoMapper;
using MyWalletApi.Dtos;
using MyWalletApi.Models;

namespace MyWalletApi.Mapping
{
    public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<User,CreateUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }

    }
}
