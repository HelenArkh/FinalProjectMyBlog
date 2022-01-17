using FinalProjectMyBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.ViewModels.Publications
{
    public class PublicationCreateViewModel
    {
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Введите название публикации")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым")]
        [DataType(DataType.Text)]
        [Display(Name = "Текст", Prompt = "Введите текст публикации")]
        public string Text { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
