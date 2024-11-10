using System.ComponentModel.DataAnnotations;

namespace MyWalletApi.Dtos
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = " UserName field is required.")]
        [StringLength(50, ErrorMessage = "UserName must be at most 50 characters.")]

        public string Username { get; set; }

        [Required(ErrorMessage = "Password field is required.")]
        public string Password { get; set; }

        public LoginUserDto(string userName,string Password)
        {
            Username = userName;
            this.Password = Password;
            
        }
    }
}
