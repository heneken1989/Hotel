using System.ComponentModel.DataAnnotations;

namespace Hotel.Dtos
{
    public class LoginDto

    {
        [Required(ErrorMessage = " User name is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = " Password is required")]
        public string Password { get; set; }
    }
}
