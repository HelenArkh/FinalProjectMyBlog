using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts.Publication
{
    /// <summary>
    /// Добавляет новую публикацию.
    /// </summary>
    public class AddPublicationRequest
    {
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Text { get; set; }
    }
}
