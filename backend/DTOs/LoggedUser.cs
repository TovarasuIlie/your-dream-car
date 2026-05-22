using backend.Enums;

namespace backend.DTOs
{
    public class LoggedUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public string Token { get; set; }
    }
}
