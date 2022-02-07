using API.Contracts.Publication;
using AutoMapper;
using FinalProjectMyBlog.Data.Queries;
using FinalProjectMyBlog.Data.Repository;
using FinalProjectMyBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// Контроллер статей
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PublicationController : ControllerBase
    {
        
        private IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private PublicationsRepository _publications;

        public PublicationController(UserManager<User> userManager, IMapper mapper,
            PublicationsRepository publications)
        {
            _userManager = userManager;
            _mapper = mapper;
            _publications = publications;
        }

        /// <summary>
        /// Добавление новой статьи
        /// </summary>
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> AddPublication([FromRoute] string id, [FromBody] AddPublicationRequest request)
        {
            var newPublication = _mapper.Map<AddPublicationRequest, Publication>(request);

            var user = await _userManager.FindByIdAsync(id);

            _publications.AddPublication(user, newPublication);

            return StatusCode(201, $"Статья {request.Title} добавлена");
        }

        /// <summary>
        /// Просмотр всех статей пользователя
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPublicationsByUser([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var publications = _publications.GetPublicationsByUser(user)
                .ToArray();

            var resp = new GetPublicationsResponse
            {
                PublicationAmount = publications.Length,
                Publications = _mapper.Map<Publication[], PublicationView[]>(publications)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Просмотр статьи по Id
        /// </summary>
        [HttpGet]
        [Route("GetPublicationsById/{id}")]
        public IActionResult GetPublicationsById([FromRoute] string id)
        {

            var publication = _publications.GetPublicationsById(id);

            var resp = new
            {
                Publication = _mapper.Map<Publication, PublicationView>(publication)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Просмотр всех статей
        /// </summary>
        [HttpGet]
        [Route("")]
        public IActionResult GetPublications()
        {
            var publications = _publications.GetAllPublications()
                .ToArray();

            var resp = new GetPublicationsResponse
            {
                PublicationAmount = publications.Length,
                Publications = _mapper.Map<Publication[], PublicationView[]>(publications)
            };

            return StatusCode(200, resp);
        }

        /// <summary>
        /// Обновление существующей статьи
        /// </summary>
        [HttpPatch]
        [Route("{id}")]
        public IActionResult EditPublication(
            [FromRoute] string id,
            [FromBody] EditPublicaRequest request)
        {
            var publication = _publications.GetPublicationsById(id);
            if (publication == null)
                return StatusCode(400, $"Ошибка: Статьи с идентификатором {id} не существует.");

            _publications.UpdatePublication(
                publication,
                new UpdatePublicationQuery(request.Title, request.Text)
            );

            return StatusCode(200, $"Статья обновлена!  Новая статья — {publication.Title}");
        }

        /// <summary>
        /// Удаление существующей статьи
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePublication(
            [FromRoute] string id)
        {
            var publication = _publications.GetPublicationsById(id);
            if (publication == null)
                return StatusCode(400, $"Ошибка: Статьи с идентификатором {id} не существует.");

            _publications.DeletePublication(
                publication
            );

            return StatusCode(200, $"Статья удалена!");
        }
    }
}
