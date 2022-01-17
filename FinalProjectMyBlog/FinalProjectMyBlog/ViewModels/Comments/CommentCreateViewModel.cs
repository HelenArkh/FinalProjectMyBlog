using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.ViewModels.Comments
{
    public class CommentCreateViewModel
    {
        [Required]
        [Display(Name = "Идентификатор публикации")]
        public string PublicationId { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [DataType(DataType.Text)]
        [Display(Name = "Текст", Prompt = "Введите текст комментария")]
        public string Text { get; set; }
    }
}
