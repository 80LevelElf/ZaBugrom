using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.InputModels.Account
{
    public class LoginInputModel
    {
        [DisplayName("Логин")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
