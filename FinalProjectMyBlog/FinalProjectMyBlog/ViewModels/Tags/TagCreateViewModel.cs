using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.ViewModels.Tags
{
    public class TagCreateViewModel
    {
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Введите название тега")]
        public string TagName { get; set; }
    }
}
