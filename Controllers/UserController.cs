using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWalletApi.Dtos;

namespace MyWalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public async Task Register(CreateUserDto createUserDto)
        {

        }
    }
}
