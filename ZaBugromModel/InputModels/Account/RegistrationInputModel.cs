using System.ComponentModel.DataAnnotations;

namespace Models.InputModels.Account
{
    public class RegistrationInputModel
    {
        [Required] 
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
