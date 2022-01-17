using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.ViewModels.Comments
{
    public class CommentEditViewModel
    {
        [Required]
        [Display(Name = "Идентификатор комментария")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [DataType(DataType.Text)]
        [Display(Name = "Текст", Prompt = "Введите текст")]
        public string Text { get; set; }
    }
}
