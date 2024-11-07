using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWalletApi.Dtos;
using MyWalletApi.UserRepository;

namespace MyWalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase

    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }
        [HttpPost]
        public async Task<ActionResult> Register([FromBody]CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }
            await _userRepository.AddUser(createUserDto);

            return Ok("user registered successfully");
            

        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> AllUsers()
        {
            var users=await _userRepository.GetAllUsers();
            return Ok(users);
        }
    }
}
