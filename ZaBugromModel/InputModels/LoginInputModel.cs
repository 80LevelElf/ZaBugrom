using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.InputModels
{
    public class LoginInputModel
    {
        [DisplayName("Логин")]
        [Required]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
