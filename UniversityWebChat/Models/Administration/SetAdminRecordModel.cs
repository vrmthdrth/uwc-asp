using System.ComponentModel.DataAnnotations;

namespace UniversityWebChat.Models.Administration
{
    public class SetAdminRecordModel
    {
        [Required]
        public string RoleName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Требуется заполнить поле AccessPassword")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Длина AccessPassword должна быть не менее 8 символов")]
        public string AccessPassword { get; set; }
    }
}