using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Form
{
    public class RegisterUser
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The name is required!")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The email is required!")]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The password is required!")]
        public string Password { get; set; }
    }
}
