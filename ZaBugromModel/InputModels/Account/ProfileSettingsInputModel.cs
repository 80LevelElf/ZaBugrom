using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Models.Data.Enums;

namespace Models.InputModels.Account
{
    public class ProfileSettingsInputModel
    {
        [DisplayName("Логин")]
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public Gender Gender { get; set; }

        [DisplayName("Старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DisplayName("Новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
