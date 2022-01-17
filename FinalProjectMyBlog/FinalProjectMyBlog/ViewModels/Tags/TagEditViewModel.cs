using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.ViewModels.Tags
{
    public class TagEditViewModel
    {
        [Required]
        [Display(Name = "Идентификатор тега")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [DataType(DataType.Text)]
        [Display(Name = "Тег", Prompt = "Отредактируйте тег")]
        public string TagName { get; set; }
    }
}
