using System.ComponentModel.DataAnnotations;

namespace Demo.PL.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "Minlength is 5")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
