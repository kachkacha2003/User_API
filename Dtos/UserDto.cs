using System.ComponentModel.DataAnnotations;

namespace MyWalletApi.Dtos
{
    public class UserDto
    {
        Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    
     
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
