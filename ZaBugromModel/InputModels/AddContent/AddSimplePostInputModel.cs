using System.ComponentModel.DataAnnotations;

namespace Models.InputModels.AddContent
{
    public class AddSimplePostInputModel: AddPostInputModel
    {
        [Required]
        public string Source { get; set; }
    }
}
