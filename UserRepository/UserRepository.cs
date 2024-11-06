using MyWalletApi.Dtos;

namespace MyWalletApi.UserRepository
{
    public class UserRepository : IUserRepository
    {
        public Task AddUser(CreateUserDto createUserDto)
        {
            return Task.CompletedTask;
        }
    }
}
