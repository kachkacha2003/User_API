using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWalletApi.Data;
using MyWalletApi.Dtos;
using MyWalletApi.UserRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyWalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase

    {
        private readonly UserDbContext data;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration configuration;
        public UserController(IUserRepository _userRepository, UserDbContext data,IConfiguration configuration)
        {
            this._userRepository = _userRepository;
            this.data = data;
            this.configuration = configuration;
        }



        //[Authorize]
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
                //return Ok(_userRepository.GenerateToken(user));


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

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login( LoginUserDto loginUser )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result=await _userRepository.LoginUser(loginUser);
            if (result != "")
            {
                return Ok(result);
            }
            return BadRequest("please write correct Username or Password");

        }
    }
}
