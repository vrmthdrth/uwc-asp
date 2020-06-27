using System.ComponentModel.DataAnnotations;

namespace UniversityWebChat.Models.Authentication
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется заполнить поле Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется заполнить поле Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}