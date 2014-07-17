using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.InputModels.Account
{
    public class RegistrationInputModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DisplayName("Логин")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
