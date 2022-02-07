using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts.Tags
{
    /// <summary>
    /// Добавляет новый тег.
    /// </summary>
    public class AddTagRequest
    {
        public string TagName { get; set; }
    }
}
