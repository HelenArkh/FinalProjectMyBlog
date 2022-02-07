using API.Contracts.Tags;
using AutoMapper;
using FinalProjectMyBlog.Data.Queries;
using FinalProjectMyBlog.Data.Repository;
using FinalProjectMyBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// Контроллер тегов
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private TagsRepository _tags;
        private IMapper _mapper;

        public TagController(TagsRepository tags, IMapper mapper)
        {
            _tags = tags;
            _mapper = mapper;
        }

        /// <summary>
        /// Просмотр всех тегов
        /// </summary>
        [HttpGet]
        [Route("")]
        public IActionResult GetTags()
        {
            var tags = _tags.GetAllTags().ToArray();

            var resp = new GetTagsResponse
            {
                TagAmount = tags.Length,
                Tags = _mapper.Map<Tag[], TagView[]>(tags)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Добавление нового тега
        /// </summary>
        [HttpPost]
        [Route("")]
        public IActionResult AddTag(AddTagRequest request)
        {           
            //var tag = await _tags.GetDeviceByName(request.Name);
            //if (device != null)
            //    return StatusCode(400, $"Ошибка: Устройство {request.Name} уже существует.");

            var newTag = _mapper.Map<AddTagRequest, Tag>(request);
            _tags.AddTag(newTag);

            return StatusCode(201, $"Тег {request.TagName} добавлен");
        }

        /// <summary>
        /// Обновление существующего тега
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public IActionResult EditTag(
            [FromRoute] string id,
            [FromBody] EditTagRequest request)
        {
            var tag = _tags.GetTagsById(id);
            if (tag == null)
                return StatusCode(400, $"Ошибка: Тега с идентификатором {id} не существует.");

            //var withSameName = await _tags.GetDeviceByName(request.NewName);
            //if (withSameName != null)
            //    return StatusCode(400, $"Ошибка: Устройство с именем {request.NewName} уже подключено. Выберите другое имя!");

            _tags.UpdateTag(
                tag,
                new UpdateTagQuery(request.TagName)
            );

            return StatusCode(200, $"Тег обновлен!  Новый тег — {tag.TagName}");
        }
    }
}
