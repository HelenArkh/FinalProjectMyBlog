using FinalProjectMyBlog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.ViewModels.Publications
{
    public class PublicationEditViewModel
    {
        [Required]
        [Display(Name = "Идентификатор публикации")]
        public string Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Введите название публикации")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Текст", Prompt = "Введите текст")]
        public string Text { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
