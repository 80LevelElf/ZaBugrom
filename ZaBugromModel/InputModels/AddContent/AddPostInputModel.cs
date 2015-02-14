using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.InputModels.AddContent
{
    public abstract class AddPostInputModel
    {
        [Required]
        public string Title { get; set; }

        public List<string> TagList { get; set; }
    }
}
