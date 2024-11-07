using System.ComponentModel.DataAnnotations;

namespace MyWalletApi.Dtos
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = " UserName field is required.")]
        [StringLength(50, ErrorMessage = "UserName must be at most 50 characters.")]

        public string Username { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password field is required.")]
        public string Password { get; set; }
    }
}
