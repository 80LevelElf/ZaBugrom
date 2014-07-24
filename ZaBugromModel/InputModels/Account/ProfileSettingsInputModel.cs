using System.ComponentModel.DataAnnotations;
using System.Web;
using Models.Data.Enums;

namespace Models.InputModels.Account
{
    public class ProfileSettingsInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        public HttpPostedFileBase AvatarPostedFile { get; set; }
    }
}
