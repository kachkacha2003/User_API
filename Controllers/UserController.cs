using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWalletApi.Data;
using MyWalletApi.Dtos;
using MyWalletApi.UserRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyWalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase

    {
        private readonly UserDbContext data;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository _userRepository,UserDbContext data)
        {
            this._userRepository = _userRepository;
            this.data = data;
        }
       

        [HttpGet]
        public async Task<ActionResult<UserDto>> AllUsers()
        {
           
            var users=await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {

            var user = await data.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                await _userRepository.DeleteUser(user);
                return Ok("user removed successfully");
            }
            else
            {
                return BadRequest("incorrect id");
            }
        }
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var exist = await data.Users.FirstOrDefaultAsync(user => user.Username==createUserDto.Username);
            if (exist != null) {
                return BadRequest("this username is already exist");
            }
            await _userRepository.AddUser(createUserDto);

            return Ok("user registered successfully");


        }
    }
}
