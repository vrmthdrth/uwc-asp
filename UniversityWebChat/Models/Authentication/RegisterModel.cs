using System.ComponentModel.DataAnnotations;

namespace UniversityWebChat.Models.Authentication
{
    public class RegisterModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется заполнить поле Surname")]
        [StringLength(50, MinimumLength = 1)]
        public string Surname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется заполнить поле Name")]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Требуется заполнить поле Age")]
        [Range(0, 100)]
        public int Age { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется заполнить поле RoleAccessPassword")]
        [DataType(DataType.Password)]
        public string RoleAccessPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется заполнить поле Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется заполнить поле Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Минимальная длина пароля - 8 символов")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется заполнить поле Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Пароли не совпадают")]
        public string PasswordConfirmation { get; set; }
    }
}