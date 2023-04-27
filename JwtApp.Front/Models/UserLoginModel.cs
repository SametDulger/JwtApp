using System.ComponentModel.DataAnnotations;

namespace JwtApp.Front.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Parola gereklidir.")]
        public string Password { get; set; } = null!;
    }
}
